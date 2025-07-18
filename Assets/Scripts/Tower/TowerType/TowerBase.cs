using System.Collections;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] public int cost;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range;
    [SerializeField] protected float patternInterval;
    protected bool isReady;
    protected WaitForSeconds wfsCache; //패턴에서 코루틴으로 굉장히 짧은 시간을 자주 기다려야한다면 최적화를 위해 wfs를 캐싱하여 사용
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }
    protected void Start()
    {
        circleCollider.radius = range;
        isReady = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (isReady)
                StartCoroutine(ShootTask());
            Debug.Log("MonsterDetected!");
        }
    }

    protected void FireBullet(Vector2 dir) //dir방향으로 나가는 탄막을 발사
    {
        GameObject obj = ObjectPool.Instance.GetObject(bulletPrefab);
        BasicBullet bullet = obj.GetComponent<BasicBullet>();
        obj.transform.position = transform.position;
        bullet.SetDir(dir);
        obj.SetActive(true);
    }

    protected abstract IEnumerator ShootTask();
}
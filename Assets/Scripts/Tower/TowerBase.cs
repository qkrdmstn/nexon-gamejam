using System.Collections;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range;
    protected bool isReady;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void Start()
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
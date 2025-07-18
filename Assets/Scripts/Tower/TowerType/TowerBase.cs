using System.Collections;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] public int cost;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range;
    [SerializeField] protected float patternInterval;
    protected bool isReady;
    protected WaitForSeconds wfsCache; //���Ͽ��� �ڷ�ƾ���� ������ ª�� �ð��� ���� ��ٷ����Ѵٸ� ����ȭ�� ���� wfs�� ĳ���Ͽ� ���
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

    protected void FireBullet(Vector2 dir) //dir�������� ������ ź���� �߻�
    {
        GameObject obj = ObjectPool.Instance.GetObject(bulletPrefab);
        BasicBullet bullet = obj.GetComponent<BasicBullet>();
        obj.transform.position = transform.position;
        bullet.SetDir(dir);
        obj.SetActive(true);
    }

    protected abstract IEnumerator ShootTask();
}
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] public int cost;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range;
    [SerializeField] GameObject rangeVisual;
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
        rangeVisual.transform.localScale = new Vector3(range * 2, range * 2, 1);
        rangeVisual.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool uiClicked = EventSystem.current.IsPointerOverGameObject();
            Vector3 screenPos = Input.mousePosition; // ��ũ�� ��ǥ (�ȼ� ����)
            screenPos.z = 10f; // ī�޶�κ����� �Ÿ� (�ݵ�� �����ؾ� ��)
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            // UI ���� Ŭ���ߴ��� Ȯ��
            if (!uiClicked || uiClicked && ((Vector2)transform.position - worldPos).magnitude > 1)
            {
                rangeVisual.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (isReady)
                StartCoroutine(ShootTask());
        }
    }

    public void ReverseRangeVisual()
    {
        rangeVisual.SetActive(!rangeVisual.activeSelf);
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
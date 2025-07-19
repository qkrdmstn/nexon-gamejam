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
        rangeVisual.transform.localScale = new Vector3(range * 2, range * 2, 1);
        rangeVisual.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool uiClicked = EventSystem.current.IsPointerOverGameObject();
            Vector3 screenPos = Input.mousePosition; // 스크린 좌표 (픽셀 기준)
            screenPos.z = 10f; // 카메라로부터의 거리 (반드시 설정해야 함)
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            // UI 위를 클릭했는지 확인
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
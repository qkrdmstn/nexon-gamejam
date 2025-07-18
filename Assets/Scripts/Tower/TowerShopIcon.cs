using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerShopIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] TowerType type;
    [SerializeField] Color greenFadeColor;
    [SerializeField] GameObject lockUI;
    Color initColor;
    Color originFadeColor;
    TowerGround touchingGround;
    Vector3 initPos;
    float initScale;
    Image image;
    bool canInstall;
    bool canPurchase;
    RectTransform rectTransform;

    static float SLOW_SCALE = 0.05f;

    private void Awake()
    {
        image = GetComponent<Image>();
        initColor = image.color;
        originFadeColor = new Color(initColor.r, initColor.g, initColor.b, 0.5f);
        initPos = transform.position;
        initScale = transform.localScale.x;
        canInstall = false;
        canPurchase = false;
        touchingGround = null;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        lockUI.SetActive(!canPurchase);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI 클릭 시작");
        SetScale(1);
        image.color = originFadeColor;
        canInstall = false;
        MapManager.Instance.SetAllTowerGroundHighlight(true);
        Time.timeScale = SLOW_SCALE;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI 클릭 해제");
        if (canInstall)
        {
            var towerObj = MapManager.Instance.GetTower(type);
            int towerCost = towerObj.GetComponent<TowerBase>().cost;
            //towerCost만큼 Gold를 소모하는 함수 호출
            towerObj.transform.position = touchingGround.transform.position;
            touchingGround.IsEmpty = false;
        }

        transform.position = initPos;
        SetScale(initScale);
        image.color = initColor;
        MapManager.Instance.SetAllTowerGroundHighlight(false);
        Time.timeScale = 1;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI 드래그 중...");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        // UI 위치 이동
        rectTransform.anchoredPosition += eventData.delta;

        // UI → 월드 좌표 변환
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, rectTransform.position);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // 물리 충돌 검사 (2D)
        Collider2D[] hit = Physics2D.OverlapPointAll(worldPos);
        if (hit.Length > 0)
        {
            bool isTowerGround = false;
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].CompareTag("TowerGround"))
                {
                    TowerGround tg = hit[i].GetComponent<TowerGround>();
                    if (tg.IsEmpty)
                    {
                        isTowerGround = true;
                        touchingGround = tg;
                        image.color = greenFadeColor;
                        canInstall = true;
                    }
                }
            }
            if (!isTowerGround)
            {
                image.color = originFadeColor;
                canInstall = false;
            }
        }
        else
        {
            image.color = originFadeColor;
            canInstall = false;
        }
    }

    public void CheckPurchase(int currentGold) //GameManager에서 Gold를 얻을 때마다 이걸 호출해야함.
    {
        if (MapManager.Instance.GetTower(type).GetComponent<TowerBase>().cost <= currentGold)
        {
            canPurchase = true;
        }
        else
        {
            canPurchase = false;
        }
        lockUI.SetActive(!canPurchase);
    }

    private void SetScale(float value)
    {
        transform.localScale = new Vector3(value, value, 1);
    }
}

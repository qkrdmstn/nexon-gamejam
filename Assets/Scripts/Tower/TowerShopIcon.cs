using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerShopIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TowerType type;
    [SerializeField] Color redFadeColor;
    [SerializeField] Color greenFadeColor;
    [SerializeField] GameObject lockUI;
    [SerializeField] Image explainUI;
    [SerializeField] Sprite explainImg;
    [SerializeField] GameObject rangeVisual;
    Color initColor;
    Vector2 installPos; //타워를 설치할 위치. 드래그 중에 갱신됨
    Vector3 initPos;
    float initScale;
    float range;
    Image image;
    bool canInstall;
    bool canPurchase;
    bool isDragging;
    RectTransform rectTransform;

    static float SLOW_SCALE = 0.05f;

    private void Awake()
    {
        image = GetComponent<Image>();
        initColor = image.color;
        initPos = transform.position;
        initScale = transform.localScale.x;
        canInstall = false;
        canPurchase = false;
        isDragging = false;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetLock();
        explainUI.gameObject.SetActive(false);
        range = MapManager.Instance.GetTowerRange(type);
        rangeVisual.transform.localScale = new Vector3(range * 2.3f, range * 2.3f, 1);
        rangeVisual.SetActive(false);
        CheckPurchase();
        GameManager.instance.OnGolded += CheckPurchase;
    }
    private void OnDestroy()
    {
        GameManager.instance.OnGolded -= CheckPurchase;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDragging)
        {
            explainUI.gameObject.SetActive(true);
            explainUI.sprite = explainImg;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        explainUI.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI 클릭 시작");
        //SetScale(1);
        rangeVisual.SetActive(true);
        explainUI.gameObject.SetActive(false);
        image.color = redFadeColor;
        canInstall = false;
        Time.timeScale = SLOW_SCALE;
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI 클릭 해제");
        if (canInstall)
        {
            var towerObj = MapManager.Instance.GetTowerObj(type);
            int towerCost = MapManager.Instance.GetTowerCost(type);
            GameManager.instance.UseGold(towerCost);
            towerObj.transform.position = installPos;
            SoundManager.Instance.PlaySFX(SFX.TOWER_PLACE);
        }

        rangeVisual.SetActive(false);
        isDragging = false;
        transform.position = initPos;
        SetScale(initScale);
        image.color = initColor;
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
        installPos = worldPos;

        // 물리 충돌 검사 (2D)
        Collider2D[] hit = Physics2D.OverlapPointAll(worldPos);
        if (hit.Length > 0)
        {
            bool isBannedAreaIncluded = false;
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].CompareTag("BannedArea"))
                {
                    isBannedAreaIncluded = true;
                    image.color = redFadeColor;
                    canInstall = false;
                    break;
                }
            }
            if (!isBannedAreaIncluded)
            {
                image.color = greenFadeColor;
                canInstall = true;
            }
        }
        else
        {
            image.color = greenFadeColor;
            canInstall = true;
        }
    }

    public void CheckPurchase() //GameManager에서 Gold가 변동할 때마다 이걸 호출해야함.
    {
        if (MapManager.Instance.GetTowerCost(type) <= GameManager.instance.gold)
        {
            canPurchase = true;
        }
        else
        {
            canPurchase = false;
        }
        SetLock();
    }

    private void SetScale(float value)
    {
        transform.localScale = new Vector3(value, value, 1);
    }

    private void SetLock()
    {
        lockUI.SetActive(!canPurchase);
    }
}

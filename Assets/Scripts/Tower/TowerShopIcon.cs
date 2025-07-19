using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerShopIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TowerType type;
    [SerializeField] Color redFadeColor;
    [SerializeField] Color greenFadeColor;
    [SerializeField] GameObject lockUI;
    [SerializeField] GameObject explainUI;
    Color initColor;
    Vector2 installPos; //Ÿ���� ��ġ�� ��ġ. �巡�� �߿� ���ŵ�
    Vector3 initPos;
    float initScale;
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
        lockUI.SetActive(!canPurchase);
        explainUI.SetActive(false);
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
            explainUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        explainUI.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI Ŭ�� ����");
        SetScale(1);
        explainUI.SetActive(false);
        image.color = redFadeColor;
        canInstall = false;
        Time.timeScale = SLOW_SCALE;
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI Ŭ�� ����");
        if (canInstall)
        {
            var towerObj = MapManager.Instance.GetTowerObj(type);
            int towerCost = MapManager.Instance.GetTowerCost(type);
            GameManager.instance.UseGold(towerCost);
            towerObj.transform.position = installPos;
        }

        isDragging = false;
        transform.position = initPos;
        SetScale(initScale);
        image.color = initColor;
        Time.timeScale = 1;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canPurchase) return;
        //Debug.Log("UI �巡�� ��...");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        // UI ��ġ �̵�
        rectTransform.anchoredPosition += eventData.delta;

        // UI �� ���� ��ǥ ��ȯ
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, rectTransform.position);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        installPos = worldPos;

        // ���� �浹 �˻� (2D)
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

    public void CheckPurchase() //GameManager���� Gold�� ������ ������ �̰� ȣ���ؾ���.
    {
        if (MapManager.Instance.GetTowerCost(type) <= GameManager.instance.gold)
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

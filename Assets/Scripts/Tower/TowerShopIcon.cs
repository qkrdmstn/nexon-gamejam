using UnityEngine;
using UnityEngine.UI;

public class TowerShopIcon : MonoBehaviour//,IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] TowerType type;
    [SerializeField] Color originFadeColor;
    [SerializeField] Color greenFadeColor;
    GameObject touchingGround;
    Vector3 initPos;
    float initScale;
    Image image;
    bool canInstall;

    static float SLOW_SCALE = 0.05f;

    private void Awake()
    {
        image = GetComponent<Image>();
        initPos = transform.position;
        initScale = transform.localScale.x;
        canInstall = false;
        touchingGround = null;
    }

    private void OnMouseDown()
    {
        SetScale(1);
        image.color = originFadeColor;
        canInstall = false;
        MapManager.Instance.SetAllTowerGroundHighlight(true);
        Time.timeScale = SLOW_SCALE;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseUp()
    {
        if (canInstall)
        {
            var towerObj = MapManager.Instance.GetTower(type);
            towerObj.transform.position = touchingGround.transform.position;
        }

        transform.position = initPos;
        SetScale(initScale);
        image.color = Color.white;
        MapManager.Instance.SetAllTowerGroundHighlight(false);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TowerGround"))
        {
            touchingGround = collision.gameObject;
            image.color = greenFadeColor;
            canInstall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TowerGround"))
        {
            image.color = originFadeColor;
            canInstall = false;
        }
    }

    private void SetScale(float value)
    {
        transform.localScale = new Vector3(value, value, 1);
    }
}

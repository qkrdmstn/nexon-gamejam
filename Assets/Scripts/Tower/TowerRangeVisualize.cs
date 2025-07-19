using UnityEngine;

public class TowerRangeVisualize : MonoBehaviour //Tower의 Model에 장착하여 실제 타워 이미지를 인지
{
    [SerializeField] GameObject rangeVisual;
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
        rangeVisual.SetActive(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
        rangeVisual.SetActive(false);
    }
}

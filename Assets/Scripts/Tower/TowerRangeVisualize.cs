using UnityEngine;

public class TowerRangeVisualize : MonoBehaviour //Tower�� Model�� �����Ͽ� ���� Ÿ�� �̹����� ����
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

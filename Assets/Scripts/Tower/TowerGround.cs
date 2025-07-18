using UnityEngine;

public class TowerGround : MonoBehaviour //Ÿ���� �� �� �ִ� �ڸ�. Ÿ���� �巡�� ����Ǵ� �Ϳ� ���� ó���� ���⼭ ���� ����.
{
    [SerializeField] GameObject highlight;

    public bool IsEmpty { get; set; }

    private void Start()
    {
        highlight.SetActive(false);
    }

    public void HighlightOn()
    {
        if (IsEmpty)
            highlight.SetActive(true);
    }

    public void HighlightOff()
    {
        highlight.SetActive(false);
    }
}

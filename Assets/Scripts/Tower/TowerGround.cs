using UnityEngine;

public class TowerGround : MonoBehaviour //타워가 들어갈 수 있는 자리. 타워가 드래그 드랍되는 것에 대한 처리는 여기서 하지 않음.
{
    [SerializeField] GameObject highlight;

    public bool IsEmpty { get; set; }

    private void Start()
    {
        IsEmpty = true;
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

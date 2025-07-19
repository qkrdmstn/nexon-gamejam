using UnityEngine;

public class AutoSortingLayer : MonoBehaviour
{
    [SerializeField] float yOffset;
    [SerializeField] int lowOrder;
    [SerializeField] int highOrder;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetDynamicSortingOrder();
    }

    void SetDynamicSortingOrder()
    {
        float yDiff = GameManager.instance.playerObj.transform.position.y - transform.position.y;
        if (yDiff > yOffset)
        {
            sr.sortingOrder = highOrder;
        }
        else
        {
            sr.sortingOrder = lowOrder;
        }
    }
}

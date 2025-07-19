using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int quantity;
    public int parrying;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GetGold(quantity);
            FindObjectOfType<Player>().GetParryingGauge(parrying);

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using UnityEngine;

public class SlowTower : TowerBase
{
    [SerializeField] int slowRate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().DecreaseMoveSpeed(slowRate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().RestoreMoveSpeed();
        }
    }

    protected override IEnumerator ShootTask()
    {
        yield return null;
    }
}

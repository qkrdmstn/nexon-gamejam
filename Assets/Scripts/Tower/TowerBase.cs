using System.Collections;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] CircleCollider2D collider;


    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            ShootTask();
            Debug.Log("MonsterDetected!");
        }
    }

    protected abstract IEnumerator ShootTask();
}
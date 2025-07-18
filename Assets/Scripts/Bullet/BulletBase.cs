using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;

    private CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            //���� ������ ������ �Լ�
            Debug.Log("Monster Damaged");
        }
        else if (collision.tag == "Player")
        {
            //�÷��̾� ������ ������ �Լ�
            Debug.Log("Play Damaged");
        }
    }
}

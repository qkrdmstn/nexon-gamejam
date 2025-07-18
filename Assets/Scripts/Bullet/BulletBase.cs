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
            //몬스터 데미지 입히는 함수
            Debug.Log("Monster Damaged");
        }
        else if (collision.tag == "Player")
        {
            //플레이어 데미지 입히는 함수
            Debug.Log("Play Damaged");
        }
    }
}

using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float originalDamage;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] float originalScale;
    [SerializeField] float parryingDamage;
    [SerializeField] float parryingScale;

    private CircleCollider2D circleCollider;
    private Rigidbody2D rigid;
    private float timer;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (timer < lifeTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Die();
        }
    }

    private void OnEnable()
    {
        timer = 0;
        rigid.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            //몬스터 데미지 입히는 함수
            collision.GetComponent<MonsterHP>().OnDamage(damage);            
            Die();
        }
        else if (collision.CompareTag("Player"))
        {
            //플레이어 데미지 입히는 함수
            collision.GetComponent<Player>().OnDamage(1);            
            Die();
        }
    }

    public void SetDir(Vector2 dir)
    {
        transform.right = dir;
    }

    public void ParryingBullet(Vector3 playerPos, float multiplier)
    {
        Vector3 dir = transform.position - playerPos;
        rigid.velocity = dir.normalized * speed * multiplier;
        transform.localScale = new Vector3(parryingScale, parryingScale);
        damage = parryingDamage;
    }

    public void Die()
    {
        timer = 55;
        ObjectPool.Instance.ReturnObject(gameObject);
        transform.localScale = new Vector3(originalScale, originalScale);
        damage = originalDamage;
    }
}

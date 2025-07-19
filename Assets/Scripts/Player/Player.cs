using System.Collections;
using UnityEngine;
public enum PlayerDamageType
{
    Monster,
    Bullet
}

public class Player : MonoBehaviour
{
    [SerializeField] private float hitDuration = 0.6f;

    [Header("Move Info")]
    public float moveSpeed = 5.65f;

    [Header("State Info")]
    public bool isDamaged;
    public bool isParrying;
    public bool isDead;

    [Header("Parrying Info")]
    public float parryingRadius;
    public float speedMultiplier;
    public float parryingGauge;
    public float parryingGaugeRecoveryTimer;
    public float parryingGaugeRecoveryInterval;
    public float parryingGaugeRecoveryValue;

    public Vector2 moveDir;
    #region Componets
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }
    #endregion

    #region States
    public PlayerAnimController playerAnimController;
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);

        idleState = new PlayerIdleState(this, stateMachine);
        moveState = new PlayerMoveState(this, stateMachine);

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerAnimController = GetComponent<PlayerAnimController>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        if (isDead) return;
        //패링
        if (Input.GetKeyDown(KeyCode.Space) && parryingGauge >= 100)
        {
            StartCoroutine(ParryingCoroutine(parryingRadius));
            parryingGauge = 0.0f;
        }

        //패링 게이지 회복
        parryingGaugeRecoveryTimer -= Time.deltaTime;
        if (parryingGaugeRecoveryTimer < 0.0f)
        {
            parryingGaugeRecoveryTimer = parryingGaugeRecoveryInterval;
            GetParryingGauge(parryingGaugeRecoveryValue);
        }
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public void OnDamage(int damage = 1)
    {
        //Change Layer & Change Color
        GameManager.instance.OnDamage(1);
        ChangePlayerLayer(7);
        StartCoroutine(DamagedProcess(hitDuration));
    }

    IEnumerator DamagedProcess(float duration)
    {
        for (int i = 0; i < 2; i++)
        {
            playerAnimController.SetMaterialColor(new Color(1, 1, 1, 0.4f));
            yield return new WaitForSeconds(duration / 4.0f);


            playerAnimController.SetMaterialColor(new Color(1, 1, 1, 1.0f));
            yield return new WaitForSeconds(duration / 4.0f);
        }
        ChangePlayerLayer(6);
        isDamaged = false;
    }

    //Todo. GameManager에서 StageOver 함수 코루틴으로 변경, 플레이어 죽음 애니메이션 재생 기다리고 UI 오픈하기
    public void Dead()
    {
        isDead = true;
        if (moveDir.x > 0)
            playerAnimController.SetCurrentAnimation(PlayerXDir.Right, PlayerYDir.Front, PlayerAnimState.Dead);
        else
            playerAnimController.SetCurrentAnimation(PlayerXDir.Left, PlayerYDir.Front, PlayerAnimState.Dead);
    }

    private IEnumerator ParryingCoroutine(float impactRadius)
    {
        isParrying = true;
        PlayerXDir xDir = PlayerXDir.Left;
        PlayerYDir yDir = PlayerYDir.Front;
        if (moveDir.x > 0) xDir = PlayerXDir.Right;
        if (moveDir.y > 0) yDir = PlayerYDir.Back;
        playerAnimController.SetCurrentAnimation(xDir, yDir, PlayerAnimState.Hit);

        Collider2D[] inRangeTarget = Physics2D.OverlapCircleAll(this.transform.position, impactRadius);
        SoundManager.Instance.PlaySFX(SFX.PARRY);
        for (int i = 0; i < inRangeTarget.Length; i++)
        {
            GameObject target = inRangeTarget[i].gameObject;
            if (target.CompareTag("Bullet"))
            {
                Debug.Log(target);
                BasicBullet bullet = target.GetComponent<BasicBullet>();
                bullet.ParryingBullet(this.transform.position, speedMultiplier);
            }
        }
        yield return new WaitForSeconds(0.47f);
        isParrying = false;
    }

    public void ChangePlayerLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public void GetParryingGauge(float val)
    {
        parryingGauge += val;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, parryingRadius);
    }
}


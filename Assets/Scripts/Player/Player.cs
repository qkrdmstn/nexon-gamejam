using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.U2D;
using Cinemachine;
public enum PlayerDamageType
{
    Monster,
    Bullet
}

public class Player : MonoBehaviour
{
    [Header("Life Info")]
    [SerializeField] float curHP = 10;
    float maxHP = 10;
    [SerializeField] private float hitDuration = 0.6f;

    [Header("Move Info")]
    public float moveSpeed = 5.65f;
    public float dashSpeed = 13.0f;
    public float dashDuration = 0.58f;
    public float dashExpCoefficient = -3.5f;

    [Header("State Info")]
    public bool isDamaged;

    [Header("Parrying Info")]
    public float parryingRadius;
    public float speedMultiplier;
    
    #region Componets
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }
    public SpriteRenderer spriteRenderer;
    public CinemachineImpulseSource impulseSource;
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);

        idleState = new PlayerIdleState(this, stateMachine);
        moveState = new PlayerMoveState(this, stateMachine);
        dashState = new PlayerDashState(this, stateMachine);

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        stateMachine.Initialize(idleState);
    }

    private void Start()
    {
        //cameraManager = FindObjectOfType<CameraManager>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        if (Input.GetKeyDown(KeyCode.Space))
            Parrying(parryingRadius);

    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public void SetVelocity(Vector2 vel)
    {
        rb.velocity = vel;
    }

    public void OnDamage(float damage)
    {
        //Change Layer & Change Color
        GameManager.instance.OnDamage(damage);
        impulseSource.GenerateImpulse();
        ChangePlayerLayer(7);
        StartCoroutine(DamagedProcess(hitDuration));
    }

    IEnumerator DamagedProcess(float duration)
    {
        for (int i = 0; i < 2; i++)
        {
            Color color = spriteRenderer.color;
            
            //적의 투명도 조정
            color.a = 0.4f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(duration / 4.0f);


            color.a = 1.0f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(duration / 4.0f);

            //animController.SetMaterialColor(new Color(1, 1, 1, 0.4f));

            //animController.SetMaterialColor(new Color(1, 1, 1, 1f));
        }
        ChangePlayerLayer(6);
        isDamaged = false;
    }

    private void Dead()
    {

    }

    private void Parrying(float impactRadius)
    {
        Collider2D[] inRangeTarget = Physics2D.OverlapCircleAll(this.transform.position, impactRadius);
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
        impulseSource.GenerateImpulse();
    }

    public void SetIdleStatePlayer()
    {
        SetVelocity(0, 0);
        stateMachine.ChangeState(idleState);
    }

    public void ChangePlayerLayer(int layer)
    {
        gameObject.layer = layer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, parryingRadius);
    }
}


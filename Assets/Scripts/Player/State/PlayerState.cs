using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    #region Components
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    #endregion

    #region Inputs
    protected static float xInput;
    protected static float yInput;
    #endregion

    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        rb = player.rb;
    }

    Vector3 centerPos = new Vector3(0.5f, 0.5f);
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    public virtual void Exit()
    {
        //player.anim.SetBool(animBoolName, false);
    }
}

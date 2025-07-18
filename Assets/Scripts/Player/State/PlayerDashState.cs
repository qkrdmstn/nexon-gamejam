using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private Vector2 dashDir;
    private Vector2 dash;
    private float dashSpeed;

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine) : base(_player, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Dash Setting
        dashDir = new Vector2(xInput, yInput);
        dashDir.Normalize();
        dashSpeed = player.dashSpeed;

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        //Exponantial
        dash = dashDir * dashSpeed * Mathf.Exp(player.dashExpCoefficient * (player.dashDuration - stateTimer));
        player.SetVelocity(dash.x, dash.y);
        //Dash Duration
        if (stateTimer < 0.0)
            stateMachine.ChangeState(player.idleState);
    }
}

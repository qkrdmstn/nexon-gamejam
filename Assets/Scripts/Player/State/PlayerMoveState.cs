using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float moveSpeed;

    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine) : base(_player, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        moveSpeed = player.moveSpeed;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if (xInput == 0 && yInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        base.Update();

        //Move
        Vector2 dir = new Vector2(xInput, yInput);
        dir.Normalize();
        player.moveDir = dir;

        moveSpeed = player.moveSpeed;
        Vector2 move = dir * moveSpeed;
        player.SetVelocity(move.x, move.y);

        if (player.isParrying) return;
        PlayerXDir xDir = PlayerXDir.Left;
        PlayerYDir yDir = PlayerYDir.Front;
        if (player.moveDir.x > 0) xDir = PlayerXDir.Right;
        if (player.moveDir.y > 0) yDir = PlayerYDir.Back;
        player.playerAnimController.SetCurrentAnimation(xDir, yDir, PlayerAnimState.Run);
    }
}

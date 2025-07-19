using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine) : base(_player, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0 || yInput != 0)
            stateMachine.ChangeState(player.moveState);

        if (player.isParrying || player.isDead) return;
        player.playerAnimController.SetCurrentAnimation(PlayerXDir.Left, PlayerYDir.Front, PlayerAnimState.Idle);

    }
}

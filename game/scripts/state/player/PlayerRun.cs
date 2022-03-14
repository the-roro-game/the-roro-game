using Godot;
using System;
using Godot.Collections;

public class PlayerRun : State
{
    public override void PhysicsUpdate(float _delta)
    {
        Player player = GetOwner<Player>();
        player.ApplyMovement(_delta);
        
        // if (player.Velocity.IsEqualApprox(new Vector2(0, 0)))
        // {
        //     StateMachine.TransitionTo("PlayerIdle");
        // }
    }

    public override void Update(float _delta)
    {
        if (Input.IsActionJustPressed("ui_distant"))
        {
            StateMachine.TransitionTo("PlayerDistantAttack");

        }
    }
}
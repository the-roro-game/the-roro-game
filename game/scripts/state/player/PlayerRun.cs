using Godot;
using System;
using Godot.Collections;

public class PlayerRun : State
{
    public override void PhysicsUpdate(float _delta)
    {
        Player player = GetOwner<Player>();
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        inputVector.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        var animatedSprite = player.GetNode<AnimatedSprite>("AnimatedSprite");
        if (inputVector != Vector2.Zero)
        {
            inputVector = inputVector.Normalized() * player.Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
            StateMachine.TransitionTo("PlayerIdle");
        }

        if (inputVector.x != 0)
        {
            animatedSprite.Animation = "right";
            animatedSprite.FlipH = inputVector.x < 0;
            animatedSprite.SpeedScale = 2f;
        }
        else if (inputVector.y != 0)
        {
            animatedSprite.Animation = inputVector.y > 0 ? "idle" : "up";
            animatedSprite.FlipH = false;
        }

        player.Velocity = inputVector * _delta;
        player.MoveAndCollide(player.Velocity);
        if (player.Velocity.IsEqualApprox(new Vector2(0, 0)))
        {
            StateMachine.TransitionTo("PlayerIdle");
        }
    }
}
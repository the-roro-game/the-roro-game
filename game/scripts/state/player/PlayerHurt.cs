using Godot;
using System;
using Godot.Collections;

public class PlayerHurt : State
{
    public override void EnterState(Dictionary<string, string> _datas = null)
    {
        GD.Print("hurt");
        Player player = GetOwner<Player>();
        player.IsInvincible = true;
        player.Speed = player.BaseSpeed / 2;
        player.GetNode<AnimatedSprite>("AnimatedSprite").Animation = "idle";
        StartHurt();
    }

    public void EnterStatDeffered(Player player)
    {
        player.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
    }

    public void ExitStateDeffered(Player player)
    {
        player.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public async void StartHurt()
    {
        Player player = GetOwner<Player>();
        AnimatedSprite animatedSprite = player.GetNode<AnimatedSprite>("AnimatedSprite");
        Tween tween = new Tween();
        tween.Name = "Tween";
        AddChild(tween);
        tween.InterpolateProperty(player, "modulate:a", 1, 0, 1f);
        tween.Start();
        await ToSignal(tween, "tween_completed");
        tween.InterpolateProperty(player, "modulate:a", 0, 1, 0.3f);

        tween.Start();
        await ToSignal(tween, "tween_completed");
        StateMachine.TransitionTo("PlayerIdle");
        GD.Print(player.IsInvincible);
    }

    public override void PhysicsUpdate(float _delta)
    {
        Player player = GetOwner<Player>();
        player.ApplyMovement(_delta);
    }

    public override void ExitState()
    {
        Player player = GetOwner<Player>();
        player.Speed = player.BaseSpeed;

        // player.Velocity = Vector2.Zero;
        player.RotationDegrees = 0;
        player.IsInvincible = false;
        // CallDeferred(nameof(ExitStateDeffered), player);
    }
}
using Godot;
using System;
using Godot.Collections;

public class PlayerHurt : State
{
    public override void EnterState(Dictionary<string, string> _datas = null)
    {
        Player player = GetOwner<Player>();
        // player.Velocity = Vector2.Zero;
        player.GetNode<AnimatedSprite>("AnimatedSprite").Animation = "idle";
        player.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        StartHurt();
    }


    public async void StartHurt()
    {
        Player player = GetOwner<Player>();
        AnimatedSprite animatedSprite = player.GetNode<AnimatedSprite>("AnimatedSprite");
        Tween tween = new Tween();
        tween.Name = "Tween";
        AddChild(tween);
        tween.InterpolateProperty(player, "modulate:a", 1, 0, 0.4f);
        tween.Start();
        await ToSignal(tween, "tween_completed");
        tween.InterpolateProperty(player, "modulate:a", 0, 1, 0.1f);

        tween.Start();
        await ToSignal(tween, "tween_completed");
        StateMachine.TransitionTo("PlayerIdle");
    }


    public override void ExitState()
    {
        Player player = GetOwner<Player>();
        player.Velocity = Vector2.Zero;
        // player.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
        player.RotationDegrees = 0;
    }
}
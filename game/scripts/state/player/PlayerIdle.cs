using Godot;
using System;
using Godot.Collections;

public class PlayerIdle : State
{
    public override void HandleInput(InputEvent _event)
    {
        if (_event.IsAction("ui_distant"))
        {
            StateMachine.TransitionTo("PlayerDistantAttack");
        }
    }

    public override void EnterState(Dictionary<string, string> _datas = null)
    {
        Player player = GetOwner<Player>();
        // player.Velocity = Vector2.Zero;
        player.GetNode<AnimatedSprite>("AnimatedSprite").Animation = "idle";
    }

    public override void Update(float _delta)
    {
        if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right") ||
            Input.IsActionPressed("move_up") || Input.IsActionPressed("move_down"))
        {
            StateMachine.TransitionTo("PlayerRun");
        }

        if (Input.IsActionJustPressed("ui_distant"))
        {
            StateMachine.TransitionTo("PlayerDistantAttack");

        }
    }
}
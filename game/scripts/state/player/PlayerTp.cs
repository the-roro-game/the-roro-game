using Godot;
using System;
using Godot.Collections;
using therorogame.scripts;

public class PlayerTp : State
{
    [Signal]
    public delegate void BeginTp();

    public delegate void EndTp();

    private int X = 0;
    private int Y = 0;

    public override void EnterState(Dictionary<string, string> _datas = null)
    {
        Player player = GetOwner<Player>();
        player.Velocity = Vector2.Zero;
        player.GetNode<AnimatedSprite>("AnimatedSprite").Animation = "idle";
        EmitSignal(nameof(BeginTp));
        if (_datas != null)
        {
            X = _datas["X"].ToInt();
            Y = _datas["Y"].ToInt();
        }

        StartTpAnimation(1f);
    }

    public override void Update(float _delta)
    {
    }


    private void StartTpAnimation(float duration)
    {
        Player player = GetOwner<Player>();
        Tween tween = new Tween();
        tween.Name = "Tween";
        tween.InterpolateProperty(player, "rotation_degrees", player.RotationDegrees, -360, duration,
            Tween.TransitionType.Back);
        tween.InterpolateProperty(player, "scale", player.Scale, new Vector2(0, 0), duration);
        tween.Connect("tween_completed", this, nameof(OnTpCompleted));

        AddChild(tween);

        tween.Start();
    }

    public void OnTpCompleted(Godot.Object obj, NodePath key)
    {
        Tween tween = GetNode<Tween>("Tween");
        tween.StopAll();
        GD.Print("lolll");
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        StateMachine.TransitionTo("PlayerIdle");

        events.EmitSignal(nameof(Events.DirectionChange), X, Y);
    }
}
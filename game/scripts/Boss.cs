using Godot;
using System;
using therorogame.scripts;

public class Boss : Mob
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private const int Rotate_speed = 100;
    private const double Shooter_timer = 0.2;
    private const int Spawn_points = 8;
    private const int Radius = 10;
    [Export] public PackedScene Bullet_scene;
    private Node2D rotater = null;
    private Timer shoot_timer = null;
    public bool CanHit = false;

    // Called when the node enters the scene tree for the first time.
    public override async void GetDamages()
    {
        Invicible = true;

        GetNode<Sprite>("Sprite").Modulate = Colors.OrangeRed;
        await ToSignal(GetTree().CreateTimer(2f), "timeout");
        GetNode<Sprite>("Sprite").Modulate = Colors.White;
        Invicible = false;
    }

    public override void _Ready()
    {
        GD.Print("boss");
        shoot_timer = GetNode<Timer>("ShootTimer");
        rotater = GetNode<Node2D>("Rotater");


        const double Step = 2 * Mathf.Pi / Spawn_points;

        const int StepI = (int) Step;

        for (int i = 0; i < Spawn_points; i++)
        {
            var spawn = new Node2D();
            var pos = new Vector2(Radius, 0).Rotated((float) StepI * i);
            spawn.Position = pos;
            spawn.Rotation = pos.Angle();
            rotater.AddChild(spawn);
        }
    }

    public void StartShooting()
    {
        CanHit = true;
        shoot_timer.WaitTime = (float) Shooter_timer;
        shoot_timer.Start();
    }

    public override void _Process(float delta)
    {
        if (CanHit)
        {
            rotater = GetNode<Node2D>("Rotater");
            circlePattern(rotater, delta);
        }
    }

    private void _on_ShootTimer_timeout()
    {
        rotater = GetNode<Node2D>("Rotater");
        // Replace with function body.
        foreach (Node2D child in rotater.GetChildren())
        {
            Node2D bullet = Bullet_scene.Instance<Node2D>();
            GetTree().CurrentScene.AddChild(bullet);
            bullet.Position = child.GlobalPosition;
            bullet.Rotation = child.GlobalRotation;
        }
    }

    private void circlePattern(Node2D rotater, float delta)
    {
        float new_rotation = rotater.RotationDegrees + Rotate_speed * delta;
        rotater.RotationDegrees = new_rotation % 360;
    }
}
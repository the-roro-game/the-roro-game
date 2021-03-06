using Godot;
using System;
using therorogame.scripts;

public class ElephantPHPMob : Mob
{
    private KinematicBody2D player = null;

    private int speed = 10;
    private String state = "fight";
    private const double Shooter_timer = 3;

    [Export(PropertyHint.Enum, "linear,loop")]
    private String patrol_type = "linear";

    PathFollow2D path = null;
    [Export()] public PackedScene Bullet_scene;
    private Timer shoot_timer = null;


    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public override void _Ready()
    {
        shoot_timer = GetNode<Timer>("ShootTimer");


        shoot_timer.WaitTime = (float) Shooter_timer;
        shoot_timer.Start();
    }

    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
        if (trigger.IsTrigger)
        {
            state = "fire";
        }
        else
        {
            state = "patrol";
            militaryPolice(delta);
        }
    }


    public void militaryPolice(float delta)
    {
        path = GetParent<PathFollow2D>();
        path.Offset += speed * delta;
    }

    private void _on_ShootTimer_timeout()
    {
        if (state != "fire") return;
        Player player = GetTree().CurrentScene.GetNode<Player>("Player");
        Bullet bullet = Bullet_scene.Instance<Bullet>();
        bullet.Position = GetNode<Position2D>("BulletPosition").GlobalPosition;
        bullet.Velocity = player.GlobalPosition - bullet.Position;
        GetTree().CurrentScene.AddChild(bullet);
        // GetParent().AddChild(bullet);
    }


   
}
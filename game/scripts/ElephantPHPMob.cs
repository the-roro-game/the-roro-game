using Godot;
using System;

public class ElephantPHPMob : KinematicBody2D
{private KinematicBody2D player = null;

    private int speed = 9;
    private String state = "fight";
    private const double Shooter_timer = 3;
    [Export(PropertyHint.Enum,"linear,loop")] private String patrol_type = "linear";
    
    PathFollow2D path = null;
    PackedScene Bullet_scene = (PackedScene) ResourceLoader.Load("res://scenes/BossPat/Bullet.tscn");
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
        InteractableTrigger trigger2 = GetNode<InteractableTrigger>("Interactable2");
        if (trigger.IsTrigger && !trigger2.IsTrigger)
        {
            state = "fight";
            surveyCorps(delta);
        }
        else if (trigger2.IsTrigger)
        {
            state = "fire";
        }
        else
        {
            state = "patrol";
            militaryPolice(delta);
        }

    }

    public void surveyCorps(float delta)
    {   
        
        var player = GetTree().CurrentScene.GetNode<KinematicBody2D>("Player");
        var move = player.Position - GlobalPosition;
        Position += move * delta;
    }

    public void militaryPolice(float delta)
    {   
        path = GetParent<PathFollow2D>();
        path.Offset += speed*delta;
        
        
    }

    private void _on_ShootTimer_timeout()
    {
        if (state != "fire") return;
        Node2D bullet = Bullet_scene.Instance<Node2D>();
        AddChild(bullet);
    }



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}


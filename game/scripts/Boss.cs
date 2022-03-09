using Godot;
using System;	

public class Boss : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private const int Rotate_speed = 100;
	private const double Shooter_timer = 0.2;
	private const int Spawn_points = 4;
	private const int Radius = 10;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var shoot_timer = GetNode<Timer>("ShootTimer");
		var rotater = GetNode<Node2D>("Rotater");
		
		

		const double Step = 2 * 3.14 / Spawn_points;
		const int StepI = (int) Step;
		
		for (int i = 0; i < Spawn_points; i++)
		{
			var spawn = new Node2D();
			var pos = new Vector2(Radius, 0).Rotated((float)StepI * i);
			spawn.Position = pos;
			spawn.Rotation = pos.Angle();
			rotater.AddChild(spawn);
			
		}

		shoot_timer.WaitTime = (float) Shooter_timer;
		shoot_timer.Start();
		
	}
	
	private void _on_ShootTimer_timeout()
	{
		var rotater = GetNode<Node2D>("Rotater");
		var Bullet_scene = (PackedScene) ResourceLoader.Load("res://scenes/BossPat/Bullet.tscn");

		
		// Replace with function body.
		foreach (var child in rotater.GetChildren())
		{
			var bullet = Bullet_scene.Instance();
			GetTree().Root.AddChild(bullet);
		}
	}


}



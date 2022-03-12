using Godot;
using System;

public class Bullet : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	const int Speed = 100;
	
	public override void _Process(float delta)
	{
		GlobalPosition += Transform.x * Speed * delta;
	}
	
	private void _on_KillTimer_timeout()
	{
		// Replace with function body.
		QueueFree();
	}
}






using Godot;
using System;
using therorogame.scripts;

public class DirectionButton : Node2D
{
	public int XOffset = 0;
	public int YOffset = 0;

	public override void _Ready()
	{
		GetNode<Area2D>("TriggerZone").Connect("body_entered", this, nameof(OnTriggerDirection));
	}

	public void OnTriggerDirection(Node node)
	{
		if (node.Name == "Player")
		{
			Events events = (Events) GetNode("/root/events");
			events.EmitSignal(nameof(Events.DirectionChange), XOffset, YOffset);
		}
	}
}

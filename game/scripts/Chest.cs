using Godot;
using System;

public class Chest : Node2D
{
    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
        if (trigger != null)
        {
            if (Input.IsActionPressed("ui_interact") && trigger.IsTrigger)
            {
                GetNode<AnimatedSprite>("sprite").Animation = "open";
                trigger.DisableCollision();
            }
        }
    }
}
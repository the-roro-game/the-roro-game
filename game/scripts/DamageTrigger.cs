using Godot;
using System;
using therorogame.scripts;

public class DamageTrigger : Area2D
{
    [Export] public int Damage = 0;

    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("interactor"))
        {
            GD.Print("lol");
            Events events = (Events) GetNode<Events>("/root/events");
            events.EmitSignal(nameof(Events.TakeDamage), Damage);
        }
    }
}
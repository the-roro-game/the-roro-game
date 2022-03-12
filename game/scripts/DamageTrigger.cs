using Godot;
using System;
using therorogame.scripts;

public class DamageTrigger : Area2D
{
    [Export] public int Damage = 0;
    [Export] public bool DeleteOnHit = false;

    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnBodyEntered));
        Connect("body_exited", this, nameof(OnBodyExited));
    }

    public void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("player") )
        {
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
            Events events = (Events) GetNode<Events>(AutoloadPath.EVENTS_PATH);
            events.EmitSignal(nameof(Events.TakeDamage), Damage);

            if (DeleteOnHit)
                GetOwner<Node2D>().QueueFree();
        }
    }

    public void OnBodyExited(Node body)
    {
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", false);
    }
}
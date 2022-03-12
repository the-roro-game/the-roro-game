using Godot;
using System;
using therorogame.scripts;

public class DamageTrigger : Node2D
{
    [Export] public int Damage = 0;
    [Export] public bool DeleteOnHit = false;

    public override void _Ready()
    {
        Connect("body_entered", this, nameof(TriggerEnter));
    }

    public void TriggerEnter(Node body)
    {
        if(DeleteOnHit)
            GetOwner<Node2D>().QueueFree();

        if (body.IsInGroup("player") && !body.GetNode<Player>(".").IsInvincible)

        {
            Events events = (Events) GetNode<Events>(AutoloadPath.EVENTS_PATH);
            events.EmitSignal(nameof(Events.TakeDamage), Damage);

          
               
        }
    }

    
}
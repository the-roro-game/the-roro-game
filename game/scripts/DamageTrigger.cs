using Godot;
using System;
using therorogame.scripts;

public class DamageTrigger : Node2D
{
    [Export] public int Damage = 0;
    [Export] public bool DeleteOnHit = false;
    [Export] public string GroupTrigger;

    public override void _Ready()
    {
        Connect("body_entered", this, nameof(TriggerEnter));
    }

    public void TriggerEnter(Node body)
    {
      
        if (body is Damageable damageable)
        {
            if (body.IsInGroup(GroupTrigger) && damageable.CanTakeDamage())
            {
                if (DeleteOnHit)
                {
                    GetOwner<Node2D>().QueueFree();
                }
                damageable.MakeDamages(Damage);

                
            }
        }
    }
}
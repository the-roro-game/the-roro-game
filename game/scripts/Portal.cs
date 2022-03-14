using Godot;

public class Portal : Node2D
{
    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
        if (Input.IsActionJustPressed("ui_interact") && trigger.IsTrigger)
        {
            trigger.DisableCollision();
            trigger.IsTrigger = false;
            TpBehaviour();
        }
    }

    public virtual void TpBehaviour()
    {
    }
}
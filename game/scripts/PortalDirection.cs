using Godot;

namespace therorogame.scripts
{
    public class PortalDirection : Node2D
    {
        public int XOffset = 0;
        public int YOffset = 0;


        public override void _Process(float delta)
        {
            InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
            if (Input.IsActionJustPressed("ui_interact") && trigger.IsTrigger)
            {
                trigger.DisableCollision();
                trigger.IsTrigger = false;
                Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
                events.EmitSignal(nameof(Events.PlayerStartTp), XOffset, YOffset);
            }
        }
    }
}
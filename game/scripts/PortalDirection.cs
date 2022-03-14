using Godot;

namespace therorogame.scripts
{
    public abstract class PortalDirection : Portal
    {
        public int XOffset = 0;
        public int YOffset = 0;


        public override void TpBehaviour()
        {
            Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
            events.EmitSignal(nameof(Events.PlayerStartTp), XOffset, YOffset);
        }
    }
}
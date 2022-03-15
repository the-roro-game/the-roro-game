using Godot;

namespace therorogame.scripts
{
    public class PortalLevelName : Portal
    {
        [Export] public string LevelName;

        public override void TpBehaviour()
        {
            LevelsManager lm = (LevelsManager) GetNode(AutoloadPath.LEVELS_MANAGER);
            lm.LoadByName(LevelName);
        }
    }
}
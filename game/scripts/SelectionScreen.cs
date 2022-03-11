using Godot;
using therorogame.data;

namespace therorogame.scripts
{
	public class SelectionScreen : Node2D
	{
		public override void _EnterTree()
		{
			Events events = (Events)GetNode(AutoloadPath.EVENTS_PATH);
			events.Connect(nameof(Events.CharacterChange), this, nameof(OnCharacterChange));
		}

		public void OnCharacterChange(BaseCharacter character)
		{
			LevelsManager lm = (LevelsManager)GetNode(AutoloadPath.LEVELS_MANAGER);
			lm.LoadLevel(0, 0);
		}
	}
}

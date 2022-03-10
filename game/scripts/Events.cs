using Godot;
using therorogame.data;

namespace therorogame.scripts
{
    public class Events : Node
    {
        [Signal]
        public delegate void CharacterChange(BaseCharacter newCharacter);

        [Signal]
        public delegate void DirectionChange(int x, int y);


        [Signal]
        public delegate void PlayerLifeChange(int NewLife, int MaxLife);

        [Signal]
        public delegate void TriggerInteract(string text);

        [Signal]
        public delegate void ExitInteract();

        [Signal]
        public delegate void Interact();
    }
}
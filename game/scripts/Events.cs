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
    }
}
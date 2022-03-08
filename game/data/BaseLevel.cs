using Godot;

namespace therorogame.data
{
    public class BaseLevel : Resource
    {
        [Export] public BaseLevel Up = null;
        [Export] public BaseLevel Right = null;
        [Export] public BaseLevel Down = null;
        [Export] public BaseLevel Left = null;
        [Export] public PackedScene LevelScene;
    }
}
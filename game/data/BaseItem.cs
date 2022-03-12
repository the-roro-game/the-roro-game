using Godot;

namespace therorogame.scripts
{
    public abstract class BaseItem : Resource
    {
        [Export] public string Name;
        [Export] public AnimatedTexture Icon;
        [Export] public string Description;
        [Export] public string SignalName = "GiveMoney";
    }
}
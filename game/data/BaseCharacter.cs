using Godot;

namespace therorogame.data
{
    public class BaseCharacter : Resource
    {
        [Export] public string Name = "toto";
        [Export] public int MaxHealth = 100;
        [Export] public int Health = 100;
        [Export] public SpriteFrames Frames;
        [Export] public string DefaultAnim = "idle";
    }
}

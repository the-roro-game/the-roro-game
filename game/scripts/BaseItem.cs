using Godot;

namespace therorogame.scripts
{
    public abstract class BaseItem : Node2D
    {
        public abstract void GetItem();

        public virtual void OnTrigger()
        {
            GD.Print("get", Name);
            GetItem();
            QueueFree();
        }
    }
}
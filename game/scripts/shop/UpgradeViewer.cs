using Godot;

namespace therorogame.scripts.shop
{
    public interface IUpgradeViewer<T>
    {
        void ViewUpgrade(T upgrade);
    }

    public abstract class UpgradeViewer<T> : Node, IUpgradeViewer<T>
    {
        public abstract void ViewUpgrade(T upgrade);
    }
}
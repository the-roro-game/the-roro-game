using Godot;

namespace therorogame.scripts.shop
{
    public interface IUpgradeList
    {
        void ClearList();
        void UpdateList();
    }

    public abstract class UpgradesList<T, V> : ScrollContainer, IUpgradeList
        where T : Resource where V : class, IUpgradeViewer<T>
    {
        [Export] public string UpgradesPath = "";
        [Export] public NodePath List;
        [Export] public NodePath Viewers;
        [Export] public PackedScene Item;
        [Export] public PackedScene Viewer;

        public override void _Ready()
        {
            UpdateList();
        }

        public void ClearList()
        {
            VBoxContainer listContainer = GetNode<VBoxContainer>(List);
            foreach (Node item in listContainer.GetChildren())
            {
                listContainer.RemoveChild(item);
                item.QueueFree();
            }
        }

        public void UpdateList()
        {
            ClearList();
            var dir = new Directory();
            if (dir.Open(UpgradesPath) == Error.Ok)
            {
                dir.ListDirBegin(true, true);
                var filename = dir.GetNext();
                while (filename != "")
                {
                    ListItem(LoadItem(filename));
                    filename = dir.GetNext();
                }

                dir.ListDirEnd();
            }
            else
            {
                GD.PrintErr("can't load upgrades items");
            }
        }

        protected T LoadItem(string path)
        {
            return ResourceLoader.Load<T>(UpgradesPath + "/" + path);
        }

        protected V PrepareViewer()
        {
            V viewer = Viewer.Instance<V>();
            
            return viewer;
        }
        

        protected abstract void ListItem(T item);
    }
}
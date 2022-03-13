using Godot;

namespace therorogame.scripts.shop
{
    public class StatsUpgradeList : UpgradesList<BaseStatModifier, StatsUpgradeViewer>
    {
        protected override void ListItem(BaseStatModifier item)
        {
            StatsItem statsItem = Item.Instance<StatsItem>();
            statsItem.UpdateItem(item);
            VBoxContainer listContainer = GetNode<VBoxContainer>(List);
            listContainer.AddChild(statsItem);
            statsItem.Connect(nameof(StatsItem.ViewStatItem), this, nameof(OnViewItem));
        }

        public void OnViewItem(BaseStatModifier modifier)
        {
            GD.Print("view ", modifier.StatName);
            StatsUpgradeViewer viewer = PrepareViewer();
            Node viewers = GetNode<Node>(Viewers);
            foreach (Node item in viewers.GetChildren())
            {
                viewers.RemoveChild(item);
                item.QueueFree();
            }

            GetNode<Node>(Viewers).AddChild(viewer);
            viewer.ViewUpgrade(modifier);
        }
    }
}
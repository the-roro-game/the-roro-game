using Godot;
using System;
using therorogame.scripts.shop;

public class ShopTabs : TabContainer
{
    public override void _Ready()
    {
        Connect("tab_changed", this, nameof(OnTabChanged));
        Connect("tab_selected", this, nameof(OnTabChanged));
    }

    public void OnTabChanged(int tab)
    {
        GetChild<IUpgradeList>(tab).UpdateList();
    }
}
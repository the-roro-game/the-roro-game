using Godot;
using System;
using therorogame.scripts;


public class CoinViewer : Control
{
    public override void _Ready()
    {
        StatsManager statsManager = (StatsManager) GetNode(AutoloadPath.STATS_PATH);
        statsManager.Connect(nameof(StatsManager.StatsChanged), this, nameof(UpdateViewer));
        UpdateViewer();
    }

    public void UpdateViewer()
    {
        StatsManager statsManager = (StatsManager) GetNode(AutoloadPath.STATS_PATH);

        GetNode<Label>("coins").Text = statsManager.Coins.ToString();
    }
}
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
        GD.Print("update coins");
        StatsManager statsManager = (StatsManager) GetNode(AutoloadPath.STATS_PATH);
        StatHandler<int> coinsStat = statsManager.GetStat<int>("CoinsStat");
        if (coinsStat != default)
        {
            GetNode<Label>("coins").Text = coinsStat.Value.ToString();
        }
    }
}
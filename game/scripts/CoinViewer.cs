using Godot;
using System;
using therorogame.scripts;
using therorogame.scripts.stats;

public class CoinViewer : Label
{
    public override void _Ready()
    {
        StatsManager statsManager = (StatsManager) GetNode(AutoloadPath.STATS_PATH);
        CoinStat coinStat = statsManager.GetStat<CoinStat>("CoinStat");
        if (coinStat != default)
        {
            coinStat.Connect(nameof(CoinStat.UpdateValue), this, nameof(OnCoinChange));
            OnCoinChange(coinStat.StatValue);
        }
        else
        {
            Visible = false;
        }
    }

    public void OnCoinChange(int NewValue)
    {
        Text = NewValue.ToString();
    }
}
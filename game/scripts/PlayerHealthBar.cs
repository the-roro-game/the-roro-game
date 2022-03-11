using Godot;
using System;
using therorogame.scripts;
using therorogame.scripts.stats;

public class PlayerHealthBar : Control
{
    public Texture GreenBar = ResourceLoader.Load<Texture>("res://arts/gui/greenbar.png");
    public Texture YellowBar = ResourceLoader.Load<Texture>("res://arts/gui/yellowbar.png");
    public Texture RedBar = ResourceLoader.Load<Texture>("res://arts/gui/redbar.png");

    public override void _Ready()
    {
        StatsManager statsManager = (StatsManager) GetNode(AutoloadPath.STATS_PATH);
        LifeStat lifeStat = statsManager.GetStat<LifeStat>("LifeStat");
        if (lifeStat != default)
        {
            lifeStat.Connect(nameof(LifeStat.UpdateLife), this, nameof(OnPlayerLifeChange));
            OnPlayerLifeChange(lifeStat.StatValue, lifeStat.MaxLife);

        }
        else
        {
            Visible = false;
        }

    }

    public void OnPlayerLifeChange(int NewLife, int MaxLife)
    {
        UpdateBar(NewLife, MaxLife);
        UpdateValues(NewLife, MaxLife);
    }


    private void UpdateBar(int NewLife, int MaxLife)
    {
        var HealthBar = GetNode<TextureProgress>("HealthBar");
        Tween tween = new Tween();
        AddChild(tween);
        tween.InterpolateProperty(HealthBar, "value", HealthBar.Value, NewLife, 1f, Tween.TransitionType.Back);
        tween.Start();
        HealthBar.MaxValue = MaxLife;
        HealthBar.TextureProgress_ = GreenBar;
        if (NewLife < MaxLife * 0.7)
        {
            HealthBar.TextureProgress_ = YellowBar;
        }

        if (NewLife < MaxLife * 0.35)
        {
            HealthBar.TextureProgress_ = RedBar;
        }

        // RemoveChild(tween);
        // tween.QueueFree();
    }

    private void UpdateValues(int NewLife, int MaxLife)
    {
        var Value = GetNode<Label>("HealthValues");
        Value.Text = $"{NewLife}/{MaxLife}";
    }
}
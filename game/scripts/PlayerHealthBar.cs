using Godot;
using System;
using therorogame.scripts;

public class PlayerHealthBar : Control
{
    public Texture GreenBar = ResourceLoader.Load<Texture>("res://arts/gui/greenbar.png");
    public Texture YellowBar = ResourceLoader.Load<Texture>("res://arts/gui/yellowbar.png");
    public Texture RedBar = ResourceLoader.Load<Texture>("res://arts/gui/redbar.png");

    public override void _EnterTree()
    {
        Events events = (Events) GetNode("/root/events");
        Global global = (Global) GetNode("/root/Global");
        events.Connect(nameof(Events.PlayerLifeChange), this, nameof(OnPlayerLifeChange));
        OnPlayerLifeChange(global.CurrCharacter.Health, global.CurrCharacter.MaxHealth);
    }

    public void OnPlayerLifeChange(int NewLife, int MaxLife)
    {
        UpdateBar(NewLife, MaxLife);
        UpdateValues(NewLife, MaxLife);
    }

    private void UpdateBar(int NewLife, int MaxLife)
    {
        var HealthBar = GetNode<TextureProgress>("HealthBar");
        HealthBar.Value = NewLife;
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
    }

    private void UpdateValues(int NewLife, int MaxLife)
    {
        var Value = GetNode<Label>("HealthValues");
        Value.Text = $"{NewLife}/{MaxLife}";
    }
}
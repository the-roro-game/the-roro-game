using Godot;
using System;
using therorogame.data;
using therorogame.scripts;

public class LevelHUD : CanvasLayer
{
    [Export] public PackedScene button;
    [Export] public PackedScene shop;


    public override void _Ready()
    {
        LevelsManager lm = (LevelsManager) GetNode(AutoloadPath.LEVELS_MANAGER);
        GetNode<Label>("HBoxContainer/CurrPos").Text = $"{lm.Y},{lm.X}";

        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.Connect(nameof(Events.TriggerInteract), this, nameof(EnableInteractText));
        events.Connect(nameof(Events.ExitInteract), this, nameof(DisableInteractText));
        events.Connect(nameof(Events.TriggerShop), this, nameof(TriggerShop));
        events.Connect(nameof(Events.CloseHud), this, nameof(CloseHud));
    }

    public void EnableInteractText(string text)
    {
        Label interactText = GetNode<Label>("InteractText");
        interactText.Visible = true;
        interactText.Text = text;
    }

    public void DisableInteractText()
    {
        Label interactText = GetNode<Label>("InteractText");
        interactText.Visible = false;
        interactText.Text = "";
    }

    public void TriggerShop()
    {
        ShopDisplay shopDisplay = shop.Instance<ShopDisplay>();
        AddChild(shopDisplay);
        GetTree().Paused = true;
    }

    public void CloseHud()
    {
        GetTree().Paused = false;
    }

    public StatsManager GetStatsManager()
    {
        return GetTree().CurrentScene.GetNode<StatsManager>("Player/StatsManager");
    }
}
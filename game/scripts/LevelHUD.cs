using Godot;
using System;
using therorogame.data;
using therorogame.scripts;

public class LevelHUD : CanvasLayer
{
    [Export] public PackedScene button;

    public override void _Ready()
    {
        LevelsManager lm = (LevelsManager) GetNode(AutoloadPath.LEVELS_MANAGER);
        GetNode<Label>("HBoxContainer/CurrPos").Text = $"{lm.Y},{lm.X}";

        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.Connect(nameof(Events.TriggerInteract), this, nameof(EnableInteractText));
        events.Connect(nameof(Events.ExitInteract), this, nameof(DisableInteractText));
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

    public StatsManager GetStatsManager()
    {
        GD.Print(GetTree().CurrentScene.GetNode<Player>("Player").GetChildren().ToString());
        return GetTree().CurrentScene.GetNode<StatsManager>("Player/StatsManager");
    }
}
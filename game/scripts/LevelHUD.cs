using Godot;
using System;
using therorogame.scripts;

public class LevelHUD : CanvasLayer
{
    [Export] public PackedScene button;

    public override void _Ready()
    {
        LevelsManager lm = (LevelsManager) GetNode("/root/lm");
        GetNode<Label>("HBoxContainer/CurrPos").Text = $"{lm.Y},{lm.X}";

        Events events = (Events) GetNode("/root/events");
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
}
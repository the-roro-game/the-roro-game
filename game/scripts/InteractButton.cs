using Godot;
using System;
using therorogame.scripts;

public class InteractButton : TouchScreenButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.Connect(nameof(Events.TriggerInteract), this, nameof(EnableButton));
        events.Connect(nameof(Events.ExitInteract), this, nameof(DisableButton));
    }


    public void EnableButton(string text)
    {
        Visible = true;
    }

    public void DisableButton()
    {
        Visible = false;
    }
}
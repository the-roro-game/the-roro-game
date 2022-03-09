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
    }
}
using Godot;
using System;
using therorogame.scripts;

public class LevelHUD : CanvasLayer
{
    [Export] public PackedScene button;

    public override void _Ready()
    {
        LevelsManager lm = (LevelsManager) GetNode("/root/lm");
        GetNode<Label>("CurrPos").Text = $"{lm.Y},{lm.X}";
        UpdateDirections();
    }


    public void UpdateDirections()
    {
        LevelsManager lm = (LevelsManager) GetNode("/root/lm");
        CanvasLayer directions = GetNode<CanvasLayer>("Directions");
        foreach (Node btn in directions.GetChildren())
        {
            directions.RemoveChild(btn);
            btn.QueueFree();
        }

        PossibleDirections possibleDirections = lm.GetPossibleDirections(lm.X, lm.Y);

        if (possibleDirections.Up)
        {
            CreateDirectionButton(0, -1, "U", Control.LayoutPreset.CenterTop);
        }

        if (possibleDirections.Right)
        {
            CreateDirectionButton(1, 0, "R", Control.LayoutPreset.CenterRight);
        }

        if (possibleDirections.Down)
        {
            CreateDirectionButton(0, 1, "D", Control.LayoutPreset.CenterBottom);
        }

        if (possibleDirections.Left)
        {
            CreateDirectionButton(-1, 0, "L", Control.LayoutPreset.CenterLeft);
        }
    }

    private void CreateDirectionButton(int x, int y, string text, Control.LayoutPreset layoutPreset)
    {
        Events events = (Events) GetNode("/roots/events");
        DirectionButton inst = (DirectionButton) button.Instance();
        AddChild(inst);
        inst.XOffset = x;
        inst.YOffset = y;
        inst.SetAnchorsAndMarginsPreset(layoutPreset, Control.LayoutPresetMode.KeepSize);
        inst.Text = text;
        inst.Connect(nameof(Events.DirectionChange),)
    }
    
    public 
}
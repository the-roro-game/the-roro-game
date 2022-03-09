using Godot;
using System;

public class DirectionGenerator : Node
{
    public Vector2 screenSize;

    [Export] public PackedScene DirectionScene;

    public override void _Ready()
    {
        screenSize = GetViewport().Size;

        UpdateDirections();
    }

    public void UpdateDirections()
    {
        LevelsManager lm = (LevelsManager) GetNode("/root/lm");
        foreach (Node2D child in GetChildren())
        {
            RemoveChild(child);
            child.QueueFree();
        }

        PossibleDirections possibleDirections = lm.GetPossibleDirections(lm.X, lm.Y);


        CreateDirectionTrigger(0, -1, "up", possibleDirections.Up, new Vector2(screenSize.x / 2, 0));
        CreateDirectionTrigger(1, 0, "right", possibleDirections.Right, new Vector2(screenSize.x, screenSize.y / 2));
        CreateDirectionTrigger(0, 1, "down", possibleDirections.Down, new Vector2(screenSize.x / 2, screenSize.y));
        CreateDirectionTrigger(-1, 0, "left", possibleDirections.Left, new Vector2(0,screenSize.y/2));
    }

    public void CreateDirectionTrigger(int x, int y, string nodeName, bool visibility, Vector2 position)
    {
        if (visibility)
        {
            DirectionButton direction = DirectionScene.Instance<DirectionButton>();
            direction.Position = position;
            direction.XOffset = x;
            direction.YOffset = y;
            direction.Name = nodeName;
            AddChild(direction);
        }
    }
}
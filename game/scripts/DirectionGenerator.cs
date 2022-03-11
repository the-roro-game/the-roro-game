using Godot;
using System;
using therorogame.scripts;

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
        LevelsManager lm = (LevelsManager) GetNode(AutoloadPath.LEVELS_MANAGER);
        foreach (Node2D child in GetChildren())
        {
            RemoveChild(child);
            child.QueueFree();
        }

        PossibleDirections possibleDirections = lm.GetPossibleDirections(lm.X, lm.Y);


        CreateDirectionTrigger(0, -1, "up", possibleDirections.Up, new Vector2(screenSize.x / 2, 64), Colors.Aquamarine);
        CreateDirectionTrigger(1, 0, "right", possibleDirections.Right,
            new Vector2(screenSize.x - 64, screenSize.y / 2), Colors.Purple);
        CreateDirectionTrigger(0, 1, "down", possibleDirections.Down, new Vector2(screenSize.x / 2, screenSize.y - 64),
            Colors.Yellow);
        CreateDirectionTrigger(-1, 0, "left", possibleDirections.Left, new Vector2(64, screenSize.y / 2), Colors.Green);
    }

    public void CreateDirectionTrigger(int x, int y, string nodeName, bool visibility, Vector2 position, Color color)
    {
        if (visibility)
        {
            DirectionButton direction = DirectionScene.Instance<DirectionButton>();
            direction.Position = position;
            direction.XOffset = x;
            direction.YOffset = y;
            direction.Name = nodeName;
            AnimatedSprite sprite = direction.GetNode<AnimatedSprite>("AnimatedSprite");
            sprite.SelfModulate = color;
            // direction.RotationDegrees = rotation;
            AddChild(direction);
        }
    }
}
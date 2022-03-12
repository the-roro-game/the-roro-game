using Godot;
using System;

public class EmptyLevel : Node2D
{
    [Export] public PackedScene CharacterScene;

    public override void _EnterTree()
    {
        var characterInst = (Player) CharacterScene.Instance();
        var startPosition = GetNode<Position2D>("StartPosition");
        characterInst.Scale = new Vector2(0.5f, 0.5f);
        AddChild(characterInst);
        characterInst.Position = startPosition.Position;
    }
}
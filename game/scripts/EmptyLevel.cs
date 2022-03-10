using Godot;
using System;

public class EmptyLevel : Node2D
{
    [Export] public PackedScene CharacterScene;

    public override void _Ready()
    {
        var characterInst = (Player) CharacterScene.Instance();
        var startPosition = GetNode<Position2D>("StartPosition");
        AddChild(characterInst);
        characterInst.Position = startPosition.Position;
    }
}
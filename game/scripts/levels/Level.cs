using Godot;
using System;

public class Level : Node2D
{
    [Export] public PackedScene CharacterScene;

    public void SpawnPlayer()
    {
        var characterInst = (Player) CharacterScene.Instance();
        var startPosition = GetNode<Position2D>("StartPosition");
        characterInst.Scale = new Vector2(0.5f, 0.5f);
        AddChild(characterInst);
        characterInst.Position = startPosition.Position;
    }

   

    public override void _Ready()
    {
        SpawnPlayer();
        GetNode<DirectionGenerator>("DirectionGenerator").UpdateDirections();
    }
}
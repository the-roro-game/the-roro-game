using Godot;
using System;
using therorogame.data;
using therorogame.scripts;

public class Player : KinematicBody2D
{
    [Export] public int Speed = 400;
    private Vector2 ScreenSize;

    public override void _Ready()
    {
        Events events = (Events) GetNode("/root/events");
        Global global = (Global) GetNode("/root/Global");
        events.Connect(nameof(Events.CharacterChange), this, nameof(OnCharacterChange));
        UpdateCharacterStyle(global.CurrCharacter);
    }

    public Vector2 Velocity = Vector2.Zero;
    
    public void UpdateCharacterStyle(BaseCharacter character)
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Frames = character.Frames;
    }


    public void OnCharacterChange(BaseCharacter character)
    {
        UpdateCharacterStyle(character);
    }
}
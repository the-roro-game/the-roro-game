using Godot;
using System;
using Godot.Collections;
using therorogame.data;
using therorogame.scripts;

public class Player : KinematicBody2D
{
    [Export] public int Speed = 400;
    private Vector2 ScreenSize;

    public override void _Ready()
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        Global global = (Global) GetNode(AutoloadPath.GLOBAL_PATH);
        events.Connect(nameof(Events.CharacterChange), this, nameof(OnCharacterChange));
        events.Connect(nameof(Events.PlayerStartTp), this, nameof(OnCharacterTp));
        events.Connect(nameof(Events.TakeDamage), this, nameof(OnCharacterTakeDamage));
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

    public void OnCharacterTp(int x, int y)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        values.Add("X", x.ToString());
        values.Add("Y", y.ToString());
        GetNode<StateMachine>("StateMachine").TransitionTo("PlayerTp", values);
    }

    public void OnCharacterTakeDamage(int damage)
    {
        GetNode<StateMachine>("StateMachine").TransitionTo("PlayerHurt");
    }
}
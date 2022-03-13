using Godot;
using System;
using Godot.Collections;
using therorogame.data;
using therorogame.scripts;

public class Player : KinematicBody2D, Damageable
{
    [Export] public int Speed = 400;
    private Vector2 ScreenSize;

    public bool IsInvincible = false;

    public override void _Ready()
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        Global global = (Global) GetNode(AutoloadPath.GLOBAL_PATH);
        events.Connect(nameof(Events.CharacterChange), this, nameof(OnCharacterChange));
        events.Connect(nameof(Events.PlayerStartTp), this, nameof(OnCharacterTp));
        events.Connect(nameof(Events.TakeDamage), this, nameof(OnCharacterTakeDamage));
        UpdateCharacterStyle(global.CurrCharacter);
    }

    public void ApplyMovement(float _delta)
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        inputVector.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        if (inputVector != Vector2.Zero)
        {
            inputVector = inputVector.Normalized() * Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
        }

        if (inputVector.x != 0)
        {
            animatedSprite.Animation = "right";
            animatedSprite.FlipH = inputVector.x < 0;
            animatedSprite.SpeedScale = 2f;
        }
        else if (inputVector.y != 0)
        {
            animatedSprite.Animation = inputVector.y > 0 ? "idle" : "up";
            animatedSprite.FlipH = false;
        }

        Velocity = inputVector * _delta;
        MoveAndCollide(Velocity);
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

    public void MakeDamages(int damages)
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.EmitSignal(nameof(Events.TakeDamage), damages);
    }

    public bool CanTakeDamage()
    {
        return !IsInvincible;
    }
}
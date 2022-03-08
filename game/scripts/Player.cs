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

    [Signal]
    public delegate void Hit();

    public override void _PhysicsProcess(float delta)
    {
        var inputVector = Vector2.Zero;
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

        MoveAndCollide(inputVector * delta);
       
    }


    public void OnPlayerBodyEntered(PhysicsBody2D body2D)
    {
        Hide();
        EmitSignal(nameof(Hit));
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }

    public void UpdateCharacterStyle(BaseCharacter character)
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Frames = character.Frames;
    }

    public void OnCharacterChange(BaseCharacter character)
    {
        UpdateCharacterStyle(character);
    }
}
using Godot;
using System;

public class SnakeMob : KinematicBody2D
{
    private KinematicBody2D player = null;

    private int speed = 80;
    private String state = "fight";
    [Export(PropertyHint.Enum,"linear,loop")] private String patrol_type = "linear";
    
    PathFollow2D path = null;
    

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    
    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
        if (trigger.IsTrigger)
        {
            state = "fight";
            surveyCorps(delta);
        }
        else
        {
            state = "patrol";
            militaryPolice(delta);
        }

    }

    public void surveyCorps(float delta)
    {   
        
        var player = GetTree().CurrentScene.GetNode<KinematicBody2D>("Player");
        var move = player.Position - GlobalPosition;
        Position += move * delta;
    }

    public void militaryPolice(float delta)
    {   
        path = GetParent<PathFollow2D>();
        path.Offset += speed*delta;

        
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

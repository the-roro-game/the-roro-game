using Godot;
using System;

public class SelectionScreen : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public void OnSelectCharacter()
    {
        var charactersList = GetNode<CharactersList>("CharactersList");
        CharacterItem characterItem = charactersList.currCharacter;
        if (characterItem == null)
        {
            charactersList.RenderMessage("no character selected");
        }
        else
        {
            charactersList.ClearMessage();
            GD.Print(charactersList.currCharacter.Name);
        }
    }
}
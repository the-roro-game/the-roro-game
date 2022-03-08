using Godot;
using therorogame.data;

namespace therorogame.scripts
{
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
            BaseCharacter characterItem = charactersList.currCharacter.Character;
            if (characterItem == null)
            {
                charactersList.RenderMessage("no character selected");
            }
            else
            {
                charactersList.ClearMessage();
                GD.Print(characterItem.Name);
            }
        }
    }
}
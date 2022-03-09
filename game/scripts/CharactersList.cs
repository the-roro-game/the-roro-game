using Godot;
using System;
using System.Collections.Generic;
using therorogame.data;
using therorogame.scripts;
using Path = System.IO.Path;

public class CharacterItem
{
	public BaseCharacter Character;
	public AnimatedSprite AnimatedSprite = new AnimatedSprite();

	public CharacterItem(BaseCharacter character)
	{
		Character = character;
		AnimatedSprite.Frames = character.Frames;
		AnimatedSprite.Animation = character.DefaultAnim;
	}
}

public class CharactersList : ItemList
{
	[Export] public string CharactersPath;

	public List<CharacterItem> CharacterItems { get; set; } = new List<CharacterItem>();
	public CharacterItem currCharacter = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("item_selected", this, nameof(OnCharacterItemSelected));
		GetNode<Button>("ValidateButton").Connect("pressed", this, nameof(OnValidateItem));

		GenerateList();
		RefreshListUi();
	}

	public void RefreshListUi()
	{
		Clear();
		foreach (var characterItem in CharacterItems)
		{
			SpriteFrames spriteFrames = characterItem.AnimatedSprite.Frames;
			AddItem(characterItem.Character.Name, spriteFrames.GetFrame(characterItem.AnimatedSprite.Animation, 0));
		}

		MaxColumns = CharacterItems.Count;
		AutoHeight = true;
		RectScale = new Vector2(1, 1);
	}

	public void GenerateList()
	{
		var dir = new Directory();
		var error = dir.Open(CharactersPath);
		if (error != Error.Ok)
		{
			GD.PrintErr("can't open characters folders");
			return;
		}

		dir.ListDirBegin(true, true);
		var file_name = dir.GetNext();
		while (file_name != "")
		{
			BaseCharacter character =
				ResourceLoader.Load<BaseCharacter>(CharactersPath + "/" + file_name);

			CharacterItem characterItem = new CharacterItem(character);
			CharacterItems.Add(characterItem);
			file_name = dir.GetNext();
		}
	}

	public void OnCharacterItemSelected(int index)
	{
		if (currCharacter != null)
		{
			currCharacter.AnimatedSprite.Animation = "idle";
		}

		currCharacter = CharacterItems[index];
		currCharacter.AnimatedSprite.Animation = "mask";
		RefreshListUi();
	}

	public void OnValidateItem()
	{
		Events events = (Events) GetNode("/root/events");
		if (currCharacter == null)
		{
			RenderMessage("No selected character");
		}
		else
		{
			ClearMessage();
			events.EmitSignal(nameof(Events.CharacterChange), currCharacter.Character);
		}
	}

	public void RenderMessage(string msg)
	{
		GetNode<Label>("ErrorMessage").Text = msg;
	}

	public void ClearMessage()
	{
		GetNode<Label>("ErrorMessage").Text = "";
	}
}

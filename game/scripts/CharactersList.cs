using Godot;
using System;
using System.Collections.Generic;
using Path = System.IO.Path;

public class CharacterItem
{
    public string Name;
    public AnimatedSprite AnimatedSprite = new AnimatedSprite();

    public CharacterItem(string name, SpriteFrames frames, string default_anim)
    {
        Name = name;
        AnimatedSprite.Frames = frames;
        AnimatedSprite.Animation = default_anim;
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
        GenerateList();
        RefreshListUi();
    }

    public void RefreshListUi()
    {
        Clear();
        foreach (var characterItem in CharacterItems)
        {
            SpriteFrames spriteFrames = characterItem.AnimatedSprite.Frames;
            AddItem(characterItem.Name, spriteFrames.GetFrame(characterItem.AnimatedSprite.Animation, 0));
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
            SpriteFrames spriteFrames = GD.Load<SpriteFrames>(CharactersPath + "/" + file_name);

            CharacterItem characterItem =
                new CharacterItem(Path.GetFileNameWithoutExtension(file_name), spriteFrames, "idle");
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

    public void RenderMessage(string msg)
    {
        GetNode<Label>("ErrorMessage").Text = msg;
    }

    public void ClearMessage()
    {
        GetNode<Label>("ErrorMessage").Text = "";
    }
}
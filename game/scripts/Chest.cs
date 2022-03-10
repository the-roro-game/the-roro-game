using Godot;
using System;
using therorogame.scripts;

public class Chest : Node2D
{
    [Export] public BaseItem item = null;
    

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
        if (trigger != null)
        {
            if (Input.IsActionPressed("ui_interact") && trigger.IsTrigger)
            {
                GetNode<AnimatedSprite>("sprite").Animation = "open";
                trigger.DisableCollision();
            }
        }
    }


    public BaseItem GetRandomItem()
    {
        var dir = new Directory();
        if (dir.Open(RootLevelPath) == Error.Ok)
        {
            dir.ListDirBegin(true, true);
            var filename = dir.GetNext();
            while (filename != "")
            {
                GD.Print(filename);
                int RoomNb = Path.GetFileNameWithoutExtension(filename).ToInt();
                if (IsValidRoom(RoomNb))
                {
                    int x = RoomNb % WIDTH;
                    int y = RoomNb / WIDTH;
                    BaseLevel level = ResourceLoader.Load<BaseLevel>(RootLevelPath + "/" + filename);
                    Map[y, x] = level;
                }

                filename = dir.GetNext();
            }

            dir.ListDirEnd();
        }
        else
        {
            GD.PrintErr("can't load map");
        }
    }
}
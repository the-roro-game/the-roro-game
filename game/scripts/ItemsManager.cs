using System;
using System.Collections.Generic;
using Godot;

namespace therorogame.scripts
{
    public class ItemsManager : Node
    {
        public static string ItemPath = "res://data/items";

        public BaseItem GetRandomItem()
        {
            List<BaseItem> items = new List<BaseItem>();
            var dir = new Directory();
            if (dir.Open(ItemPath) == Error.Ok)
            {
                dir.ListDirBegin(true, true);
                var filename = dir.GetNext();
                while (filename != "")
                {
                    items.Add(ResourceLoader.Load<BaseItem>(ItemPath + "/" + filename));
                    filename = dir.GetNext();
                }

                dir.ListDirEnd();
            }
            else
            {
                GD.PrintErr("can't load items");
                return null;
            }

            Random random = new Random();
            return items[random.Next(items.Count)];
        }
    }
}
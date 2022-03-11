using Godot;
using System;
using therorogame.scripts.stats;

public class StatsManager : Node
{
    public override void _Ready()
    {
    }

    // [Export] public string StatsPath = "res://scripts/all";
    //
    // public void LoadStats()
    // {
    //     var dir = new Directory();
    //     if (dir.Open(StatsPath) == Error.Ok)
    //     {
    //         dir.ListDirBegin(true, true);
    //         var filename = dir.GetNext();
    //         while (filename != "")
    //         {
    //             IBaseStat stat = ResourceLoader.Load<IBaseStat>(StatsPath + filename);
    //             filename = dir.GetNext();
    //         }
    //
    //         dir.ListDirEnd();
    //     }
    //     else
    //     {
    //         GD.PrintErr("can't load stats");
    //     }
    // }

    public T GetStat<T>(string name) where T : class, IBaseStat
    {
        GD.Print(GetChildren());
        if (!HasNode(name))
        {
            return default;
        }

        return GetNode<T>(name);
    }

    public void RestoreStat<T>(string name) where T : class, IBaseStat
    {
        IBaseStat stat = GetStat<T>(name);
        stat?.ResetStat();
    }
}
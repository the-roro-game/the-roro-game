using Godot;
using System;
using therorogame.data;

public class LevelsManager : Node
{
    private BaseLevel _currLevel;

    public BaseLevel CurrLevel
    {
        get => _currLevel;
        set
        {
            GD.Print("changing level");
            _currLevel = value;
            GetTree().ChangeSceneTo(_currLevel.LevelScene);
        }
    }

    [Export] public BaseLevel StartLevel;

    public static string RootLevelPath = "res://data/levels";
    public static string StartLevelName = "0";

    public void LoadLevel(string level)
    {
        BaseLevel loadedLevel =
            ResourceLoader.Load<BaseLevel>(RootLevelPath + "/" + level+".tres");
        CurrLevel = loadedLevel;
    }
}
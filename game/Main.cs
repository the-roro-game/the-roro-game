using Godot;
using System;

public class Main : Node2D
{
    [Export] public PackedScene MobScene;
    private int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            GetNode<HUD>("HUD").UpdateScore(score);
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Randomize();
        NewGame();
    }

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();
    }

    public void NewGame()
    {
        Score = 0;
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<HUD>("HUD").ShowMessage("Get ready");

        GetNode<Timer>("StartTimer").Start();
    }

    public void OnScoreTimerTimeout()
    {
        Score++;
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    public void OnMobTimerTimeout()
    {
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = GD.Randi();

        var mob = (Mob) MobScene.Instance();
        AddChild(mob);

        float dir = mobSpawnLocation.Rotation + Mathf.Pi / 2;
        mob.Position = mobSpawnLocation.Position;
        mob.Rotation = dir;

        var vel = new Vector2((float) GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = vel.Rotated(dir);
    }
}
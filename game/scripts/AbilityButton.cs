using Godot;
using System;

public class AbilityButton : TouchScreenButton
{
    private Label _timeLabel;
    private Timer _timer;
    private TextureProgress _cooldownText;
    [Export] public float CooldownTime;

    public override void _Ready()
    {
        _timeLabel = GetNode<Label>("Counter/Value");
        _timer = GetNode<Timer>("Timer");
        _cooldownText = GetNode<TextureProgress>("CooldownTexture");

        Connect("pressed", this, nameof(OnAbilityButtonPressed));
        _timer.Connect("timeout", this, nameof(OnTimerTimeout));

        _timer.WaitTime = CooldownTime;
        _timeLabel.Hide();
        _cooldownText.TextureProgress_ = Normal;
        _cooldownText.Value = 0;
        SetProcess(false);
    }

    public override void _Process(float delta)
    {
        _timeLabel.Text = Math.Round(_timer.TimeLeft, 2).ToString();
        _cooldownText.Value = (int) ((_timer.TimeLeft / CooldownTime) * 100);
        GD.Print(_cooldownText.Value);
    }

    public void OnAbilityButtonPressed()
    {
        SetBlockSignals(true);
        SetProcess(true);
        _timer.Start();
        _timeLabel.Show();
    }

    public void OnTimerTimeout()
    {
        _cooldownText.Value = 0;
        SetBlockSignals(false);
        _timeLabel.Hide();
        SetProcess(false);
    }
}
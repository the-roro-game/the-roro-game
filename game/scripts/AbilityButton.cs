using Godot;
using System;

public class AbilityButton : TextureButton
{
    private Label _timeLabel;
    private Timer _timer;
    // private TextureProgress _cooldownText;
    [Export] public float CooldownTime;

    public bool CanHit()
    {
        return _timer.TimeLeft == 0.0f;
    }

    public override void _Ready()
    {
        _timeLabel = GetNode<Label>("Counter/Value");
        _timer = GetNode<Timer>("Timer");
        // _cooldownText = GetNode<TextureProgress>("CooldownTexture");

        Connect("pressed", this, nameof(OnAbilityButtonPressed));
        _timer.Connect("timeout", this, nameof(OnTimerTimeout));

        _timer.WaitTime = CooldownTime;
        _timeLabel.Hide();
        // _cooldownText.TextureProgress_ = TextureNormal;

        // _cooldownText.Value = 0;
        SetProcess(false);
    }

    public override void _Process(float delta)
    {
        _timeLabel.Text = Math.Round(_timer.TimeLeft, 2).ToString();
        // _cooldownText.Value = (int) ((_timer.TimeLeft / CooldownTime) * 100);
        // GD.Print(_cooldownText.Value);
    }

    public void OnAbilityButtonPressed()
    {
        Input.ActionPress("ui_distant");
        GD.Print("pressed touch");
        Disabled = true;
        SetProcess(true);
        _timer.Start();
        _timeLabel.Show();
    }

    public void OnTimerTimeout()
    {
        // _cooldownText.Value = 0;
        Disabled = false;
        _timeLabel.Hide();
        SetProcess(false);
    }
}
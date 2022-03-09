using Godot;
using System;

public class HUD : CanvasLayer
{
	[Signal]
	public delegate void StartGame();

	public void ToggleControl()
	{
		GetNode<Control>("DirectionnalCross").Visible = !GetNode<Control>("DirectionnalCross").Visible;
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game over lol");
		var msgTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(msgTimer, "timeout");

		var message = GetNode<Label>("Message");
		message.Text = "jeu tah laureat";
		message.Show();
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		ToggleControl();

		GetNode<Button>("StartButton").Show();
	}

	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}

	public void ShowMessage(string msg)
	{
		var message = GetNode<Label>("Message");
		message.Text = msg;
		message.Show();
		GetNode<Timer>("MessageTimer").Start();
	}

	//signals

	public void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		ToggleControl();
		EmitSignal("StartGame");
	}

	public void OnMessageTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}
}

using Godot;
using System;

public class FloatingText : Label
{
    public async void ShowValue(string text, Vector2 travel, int duration, double spread)
    {
        Text = text;
        var movement = travel.Rotated((float) GD.RandRange(-spread / 2, spread / 2));
        RectPivotOffset = RectSize / 2;
        Tween tween = new Tween();
        AddChild(tween);

        tween.InterpolateProperty(this, "rect_position", RectPosition, RectPosition + movement, duration);
        tween.InterpolateProperty(this, "modulate:a", 1.0, 0.0, duration);
        tween.Start();
        await ToSignal(tween, "tween_all_completed");
        QueueFree();
    }
}
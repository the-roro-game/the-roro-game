using Godot;
using System;
using therorogame.scripts;

public class FloatingTextManager : Node2D
{
    [Export] public PackedScene FloatingText;

    [Export] public Vector2 travel = new Vector2(0, -80);
    [Export] public int duration = 2;
    [Export] public double spread = Math.PI;

    public override void _Ready()
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.Connect(nameof(Events.TakeDamage), this, nameof(EmitDamage));
    }

    public void EmitDamage(int damage)
    {
        var floating = FloatingText.Instance<FloatingText>();
        AddChild(floating);
        floating.ShowValue(damage.ToString(), travel, duration, spread);
    }
}
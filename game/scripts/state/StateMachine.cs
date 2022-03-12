using Godot;
using System;
using Godot.Collections;

public class StateMachine : Node
{
    [Signal]
    public delegate void Transitionned(string state_name);

    [Export] public NodePath InitialState;

    public State CurrState;

    public override void _Ready()
    {
        CurrState = GetNode<State>(InitialState);
        foreach (State state in GetChildren())
        {
            state.StateMachine = this;
        }

        CurrState.EnterState();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        CurrState.HandleInput(@event);
    }

    public override void _Process(float delta)
    {
        CurrState.Update(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        CurrState.PhysicsUpdate(delta);
    }

    public void TransitionTo(string TargetName, Dictionary<string, string> msg = null)
    {
        if (!HasNode(TargetName))
        {
            return;
        }

        CurrState.ExitState();
        CurrState = GetNode<State>(TargetName);
        CurrState.EnterState(msg);
        EmitSignal(nameof(Transitionned), CurrState.Name);
    }
}
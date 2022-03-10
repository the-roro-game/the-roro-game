using Godot;
using System;
using Godot.Collections;

public  class State : Node
{
    public StateMachine StateMachine = null;

    public virtual void HandleInput(InputEvent _event)
    {
    }

    public virtual void Update(float _delta)
    {
    }

    public virtual void PhysicsUpdate(float _delta)
    {
    }

    public virtual void EnterState(Dictionary<string, string> _datas = null)
    {
    }

    public virtual void ExitState()
    {
    }
}
using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public IStateMachine StateMachine;

    protected virtual void Update()
    {
        if(StateMachine != null) StateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        if (StateMachine != null) StateMachine.FixedUpdate();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected SharkBoss sharkBoss;
    public abstract void Tick();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(SharkBoss enemy)
    {
        this.sharkBoss = enemy;
    }

}

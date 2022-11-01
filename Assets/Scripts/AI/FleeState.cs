using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{//this calls the constructor of the base class, in our case the state, (the state needs to know what the enemy is)
    public FleeState(SharkBoss enemy) : base(enemy)
    {

    }

    public override void Tick()
    {
        sharkBoss.Flee();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            sharkBoss.SetState(new PatrolState(sharkBoss));
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Flee");
    }
}

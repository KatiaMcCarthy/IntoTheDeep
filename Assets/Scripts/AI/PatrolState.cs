using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    //this calls the constructor of the base class, in our case the state, (the state needs to know what the enemy is)
    //set one up for each type of enemy that can use this system
    public PatrolState(SharkBoss enemy) : base(enemy)
    {

    }

    //equivilant to update
    public override void Tick()
    {
        sharkBoss.Move();

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sharkBoss.SetState(new FleeState(sharkBoss));
        }
    }

    //optional, is called when we enter the state
    public override void OnStateEnter()
    {
        Debug.Log("Entered patrol");
    }
}

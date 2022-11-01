using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    //this calls the constructor of the base class, in our case the state, (the state needs to know what the enemy is)
    public ChaseState(SharkBoss enemy) : base(enemy)
    {

    }

    public override void Tick()
    {
        sharkBoss.Chase();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sharkBoss.SetState(new PatrolState(sharkBoss));
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Chase");
    }
}

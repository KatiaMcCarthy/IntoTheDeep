using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : State
{
    //this calls the constructor of the base class, in our case the state, (the state needs to know what the enemy is)
    public RangedAttackState(SharkBoss enemy) : base(enemy)
    {

    }

    public override void Tick()
    {
        sharkBoss.RangedAttack();

        //if(distance > melleEnemy.attackRange


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sharkBoss.SetState(new PatrolState(sharkBoss));
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Ranged Attack");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackThree : State
{
    //this calls the constructor of the base class, in our case the state, (the state needs to know what the enemy is)
    public SpecialAttackThree(SharkBoss enemy) : base(enemy)
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        sharkBoss.SpecialAttackThree();
    }
}

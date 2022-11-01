using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackOne : State
{
    //this calls the constructor of the base class, in our case the state, (the state needs to know what the enemy is)
    public SpecialAttackOne(SharkBoss enemy) : base(enemy)
    {

    }

    public override void Tick()
    {
        sharkBoss.SpecialAttackOne();

    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Special Attack One");
    }

    public override void OnStateExit()
    {
        sharkBoss.OnResetChargeCall();
    }
}

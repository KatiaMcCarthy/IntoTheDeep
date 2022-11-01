using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SharkBoss : BossEnemy
{
    public Action<float> OnMoveCall;
    public Action OnAttackCall;
    public Action<float> OnChaseCall;
    public Action<float> OnFleeCall;
    public Action OnPrepareChargeCall;
    public Action OnChargeAttackCall;
    public Action OnSwipeAttackCall;
    public Action OnRangedAttackCall;
    public Action OnResetChargeCall;
    //hiding for now, need to figure out when we need to intigrate this

    [HideInInspector]
    public float attackRange;
    public override void Start()
    {
        base.Start();
        //initalize stuff for melle attack/ boss attacks these are on seperate scripts
        //attackRange = GetComponent<MelleeAttack>().GetAttackRange();

        //delay on ai, to make sure stuff is assinged, allows for entry animation
        Invoke("StartBossBehaviour", 1);


    }

    protected void StartBossBehaviour()
    {
        GetComponent<SharkBrain>().patrolWeight = 100;
        GetComponent<SharkBrain>().DecideState();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Move()
    {
        Debug.Log("Move");
        if (OnMoveCall != null)
        {
            OnMoveCall(speed);
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
        if (OnAttackCall != null)
        {
            OnAttackCall();
        }
    }

    public void RangedAttack()
    {
        Debug.Log("Ranged Attack");
        if(OnRangedAttackCall != null)
        {
            OnRangedAttackCall();
        }
    }
    public void Chase()
    {
        Debug.Log("Chase");
        if(OnChaseCall != null)
        {
            OnChaseCall(speed);
        }
    }

    public void Flee()
    {
        Debug.Log("Flee");
        if (OnFleeCall != null)
        {
            OnFleeCall(fleeSpeed);
        }
    }

    public void SpecialAttackOne()
    {
        Debug.Log("Flee");
        if (OnPrepareChargeCall != null)
        {
            OnPrepareChargeCall();
        }
    }

    public void SpecialAttackTwo()
    {
        Debug.Log("Special Attack Two");
        if (OnChargeAttackCall != null)
        {
            OnChargeAttackCall();
        }
    }

    public void SpecialAttackThree()
    {
        Debug.Log("Special Attack Three");
        if (OnSwipeAttackCall != null)
        {
            OnSwipeAttackCall();
        }
    }

    public void ResetChargeAttack()
    {
        Debug.Log("Reset Charge");
        if (OnResetChargeCall != null)
        {
            OnResetChargeCall();
        }
    }

    public Transform GetPlayer()
    {
        return player;
    }

}

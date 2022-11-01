using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SharkBoss))]
public class SharkBrain : MonoBehaviour
{
    public float attackWeight;
    public float patrolWeight;
    public float chaseWeight;
    public float rangedWeight;
    public float fleeWeight;
 
    public float chargeLungeWeight;
    public float lungeWeight;
    public float swipeWeight;

    //limitation is that it needs to be in the same order in ammount
    public void DecideState()
    {
        float i = Random.Range(0, attackWeight + patrolWeight + chaseWeight + rangedWeight + fleeWeight + chargeLungeWeight + lungeWeight + swipeWeight);

        Debug.Log(i);

        if(i == 0)
        {
            DecideState();
        }
        else if (i > 0 && i <= attackWeight) //30
        {
            GetComponent<SharkBoss>().SetState(new AttackState(GetComponent<SharkBoss>()));
        }
        else if (i > attackWeight && i <= patrolWeight)
        {
            GetComponent<SharkBoss>().SetState(new PatrolState(GetComponent<SharkBoss>()));
        }
        else if (i > patrolWeight && i <= chaseWeight) //cw = 10
        {
            GetComponent<SharkBoss>().SetState(new ChaseState(GetComponent<SharkBoss>()));
        }
        else if (i > chaseWeight && i <= rangedWeight)
        {
            GetComponent<SharkBoss>().SetState(new RangedAttackState(GetComponent<SharkBoss>()));
        }
        else if (i > rangedWeight && i <= fleeWeight) // fw = 30
        {
            GetComponent<SharkBoss>().SetState(new FleeState(GetComponent<SharkBoss>()));
        }
        else if (i > fleeWeight && i <= chargeLungeWeight)
        {
            GetComponent<SharkBoss>().SetState(new SpecialAttackOne(GetComponent<SharkBoss>()));
        }
        else if (i > chargeLungeWeight && i <= lungeWeight)
        {
            GetComponent<SharkBoss>().SetState(new SpecialAttackTwo(GetComponent<SharkBoss>()));
        }
        else if (i > lungeWeight && i <= swipeWeight)
        {
            GetComponent<SharkBoss>().SetState(new SpecialAttackThree(GetComponent<SharkBoss>()));
        }
        else
        {
            //defaults to this
            GetComponent<SharkBoss>().SetState(new PatrolState(GetComponent<SharkBoss>()));
        }
    }

    public void ResetBrain()
    {
        attackWeight = 0.0f;
        patrolWeight = 0.0f;
        chaseWeight = 0.0f;
        rangedWeight = 0.0f;
        fleeWeight = 0.0f;
        chargeLungeWeight = 0.0f;
        lungeWeight = 0.0f;
        swipeWeight = 0.0f;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeBetween;
    private float currentTime;

    public Boss_Patrol bP;

    public float timeToAddOnDamage;
    
    //TODO: put this as an interface, gonna hard code it temporarilly
    //TODO: youtube video on a global timer system, for cooldowns (not global)

    public void Start()
    {
        bP = GetComponentInChildren<Boss_Patrol>();    
    }

    public void Update()
    {
        if (Time.time >= currentTime)
        {
            currentTime = Time.time + timeBetween;
            //do the thing
            bP.b_bossAttackTrigger = true;
        }

    }

    public void ReduceCurrentTime()
    {
        currentTime = currentTime - timeToAddOnDamage; //reduces current time by timeToAdd (ie it happens sooner)
    }

}

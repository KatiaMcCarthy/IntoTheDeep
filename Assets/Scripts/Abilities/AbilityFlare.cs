using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFlare : Ability
{
    public GameObject flareProjectile;
    
    public GameObject abilityFirePoint;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        GetFirePoint();
    }

    // Update is called once per frame
    void Update()
    {
        abilityVisualCooldown.fillAmount = (abilityTime - Time.time) / cooldown; //sets it to the precent of the cooldown

        if (Input.GetKeyDown(KeyCode.F) && (Time.time >= abilityTime)) //if we have the cooldown and the the button pressed
        {

            abilityTime = Time.time + cooldown;
            abilityVisualCooldown.fillAmount = 1;
           
            if(abilityFirePoint == null)
            {
                GetFirePoint();
            }

            AbilityEffect();
        }

    }

    void AbilityEffect()
    {
        Instantiate(flareProjectile, abilityFirePoint.transform.position, abilityFirePoint.transform.rotation);
    }

    public void GetFirePoint()
    {
        abilityFirePoint = GameObject.FindGameObjectWithTag("abilityFirePoint");
    }

    public void ClearFirePoint()
    {
        abilityFirePoint = null;
    }
}

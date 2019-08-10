using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : Ability
{

    private GameObject player;
    private PlayerScript pS;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player"); //Gets a refference to the player
        pS = player.GetComponent<PlayerScript>();
    }

// Update is called once per frame
    void Update()
    {
        abilityVisualCooldown.fillAmount = (abilityTime - Time.time)/cooldown; //sets it to the precent of the cooldown
       
        if (Input.GetKeyDown(KeyCode.E) && (Time.time >= abilityTime)) //if we have the cooldown and the the button pressed
        {
         
            abilityTime = Time.time + cooldown;
            abilityVisualCooldown.fillAmount = 1;

            AbilityEffect();
        }

    }

    void AbilityEffect()
    {
        pS.speed = pS.speed * buffAmmount;
        StartCoroutine(AbilityDuration());
    }

    void ResetToDefault()
    {
        pS.speed = pS.speed / buffAmmount;
    }
    IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(abilityDuration);
        ResetToDefault();
    }
}

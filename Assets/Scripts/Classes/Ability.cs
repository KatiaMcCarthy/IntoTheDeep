using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    //properties of the ability
    public float abilityTime;
    public float abilityDuration;
    public float cooldown;
    public float range;
    public float damage;
    public float buffAmmount;
    public string description;

    public GameObject castEffect; //particle effect tied to the abilitys cast;
    public GameObject hitEffect; //particle effect tied to the ability hitting;
    public GameObject deathEffect; //particle effect tied to the ability expiring;

    public Image abilityVisualCooldown; //the vvisual image that shows the cooldown

    public virtual void Start()
    {
        buffAmmount = (100 + buffAmmount)/100 ; //converts it to a %
    }
}

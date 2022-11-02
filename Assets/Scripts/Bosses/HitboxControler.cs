using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitboxControler : MonoBehaviour
{
    //this script maganages hitboxes for attacks; This script will be listening for events which control the enabeling / disabeling of hitboxes

    //Defaulting setup for 5 hitbboxes, but you could add more 
    public GameObject hitboxOne;
    public GameObject hitboxTwo;
    public GameObject hitboxThree;
    public GameObject hitboxFour;
    public GameObject hitboxFive;

    public GameObject hitboxBase; //this is the base hitbox, your general body hitbox

    List<GameObject> hitboxes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        hitboxes.Add(hitboxOne);
        //hitboxes.Add(hitboxTwo);
        //hitboxes.Add(hitboxThree);
        //hitboxes.Add(hitboxFour);
        //hitboxes.Add(hitboxFive);

        foreach (GameObject item in hitboxes)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //to be called when a boss lundges at the player
    public void OnLungeAttack()
    {
        hitboxOne.SetActive(true); // the first hitbox, in our case the lunge attacks

        Debug.Log("has lunged attacked");
    }

    public void OnFinnishLungeAttack()
    {
        hitboxOne.SetActive(false);
    }
}

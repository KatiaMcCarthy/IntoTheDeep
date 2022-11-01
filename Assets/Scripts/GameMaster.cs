using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script that contains general mechanics, other than wave spawning


public class GameMaster : MonoBehaviour
{

    public Text timerText; //todo add in a timer
    public int timeToDive = 30;  //time in seconds before you can dive
    public float diveTime = 0;
    public bool canDive = false;
    public bool pressedDive = false;  //bool that holds if the player hit dive

    public bool timerActive = false;
    public float time;

    public qaControl qa;

    public Image diveText;

    // Start is called before the first frame update
    void Start()
    {
        diveText.gameObject.SetActive(false); //initalizes the dive text
        diveTime = timeToDive;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        EndWave();
        diveTime = timeToDive - time;  //this is the value that will be outputed to the screen
    }

    //this is correctly calling every time a new wave spawns

    public void SetDive(int timeToDive)
    {
        canDive = false; //a new wave has spawned so you cannot dive
        timerActive = true;

        Debug.Log("wave start");
    }

    public void Timer()
    {
        //we want to have a timer that starts every time a wave starts
        // this timer controls a bool, that when triggerd allows the player to dive

        if (timerActive)
        {
            time += Time.deltaTime;
        }

        //sets the timer to be off if dive time hits and resets it
        if (time >= timeToDive)
        {
            timerActive = false; //stops counting the time futher
            canDive = true; //allows you to dive, this is the point that you are allowed to dive, still checked against the enemys left
            diveText.gameObject.SetActive(true);
            time = 0.0f;
        }
    }

    private void EndWave()
    {
        if(Input.GetKeyDown(KeyCode.V) && canDive == true)
        {
            qa.KillEnemys();
            pressedDive = true;  //this is the point that the wave advances
            canDive = false;
        }
    }

    public void hardResetTimer()
    {
        time = 0;
        diveText.gameObject.SetActive(false);
    }
}

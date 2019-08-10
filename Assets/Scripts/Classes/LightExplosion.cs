using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class LightExplosion : MonoBehaviour
{

    private Light2D myLight;
    public float pulseSpeed = 1f; //here, a value of 0.5f would take 2 seconds and a value of 2f would take half a second
    public float targetIntensity = 8f;
    private float currentIntensity;

    private bool b_ReachTarget;

    void Start()
    {
        myLight = this.GetComponent<Light2D>();
    }
    void Update()
    {
        if (!b_ReachTarget)
        {
            currentIntensity = Mathf.MoveTowards(myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
            myLight.intensity = currentIntensity;
            if (currentIntensity == targetIntensity)
            {
                b_ReachTarget = true;
                targetIntensity = 0;
                pulseSpeed = 30;
            }
        }

        if (b_ReachTarget)
        {
            Debug.Log("shrinking");
            currentIntensity = Mathf.MoveTowards(myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
            myLight.intensity = currentIntensity;
            if (currentIntensity == targetIntensity)
            {
                return; //stops the update
            }
        }



           
        

    }
}



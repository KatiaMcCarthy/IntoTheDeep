using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class lightPulse : MonoBehaviour
{

    private Light2D myLight;
    public float maxIntensity = 1f;
    public float minIntensity = 0f;
    public float pulseSpeed = 1f; //here, a value of 0.5f would take 2 seconds and a value of 2f would take half a second
    private float targetIntensity = 1f;
    private float currentIntensity;


    void Start()
    {
        myLight = this.GetComponent<Light2D>();
        //myLight.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        pulseSpeed = Random.Range(0.5f, 1.5f);
    }
    void Update()
    {
        currentIntensity = Mathf.MoveTowards(myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }
        myLight.intensity = currentIntensity;
    }
}



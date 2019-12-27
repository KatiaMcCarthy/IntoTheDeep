using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionIndicator : MonoBehaviour
{

    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            this.transform.rotation = player.transform.rotation;
        }

    }
}

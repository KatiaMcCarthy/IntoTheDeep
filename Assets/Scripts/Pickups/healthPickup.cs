using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    PlayerScript player;
    public int healAmmount;
    public int lifeTime;

    public GameObject pickupParticle;

    private void Start()
    {
            Invoke("DestroyPickup", lifeTime);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.Heal(healAmmount);
            Instantiate(pickupParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void DestroyPickup()
    {
        Destroy(this.gameObject);
    }
}

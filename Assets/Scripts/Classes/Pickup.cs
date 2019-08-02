using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;
    public float lifeTime = 0.0f;

    private void Start()
    {
        Invoke("DestroyPickup", lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerScript>().ChangeWeapon(weaponToEquip);
            Destroy(this.gameObject);
        }
    }

    void DestroyPickup()
    {
        Destroy(this.gameObject);
    }
}

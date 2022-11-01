using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public Transform m_transform;
    [HideInInspector]
    public Transform playerAttackPoint; //the point on the character model for the enemy to attack


    public int pickupChance; //between 0 and 100, the chance a weapon spawns
    public int healthPickupChance;

    public GameObject healthPickup;
    public GameObject[] pickups;

    //visuals
    public GameObject enemyDeathEffect;

    [HideInInspector]
    public PlayerScript playerScript;
    [HideInInspector]
    public PropertyPlayerHealth playerHealth;

    protected State currentState;

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public virtual void Start()
    {
        m_transform = this.gameObject.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponent<PlayerScript>();
        playerHealth = player.GetComponent<PropertyPlayerHealth>();

        playerAttackPoint = GameObject.FindGameObjectWithTag("PlayerAttackPoint").transform;


    }
    protected virtual void Update()
    {
        //adding safty check while we port all ai over to the new system
        if(currentState != null)
        currentState.Tick();
    }

    public void TakeDamage(int damageAmmount)
    {
        health -= damageAmmount;

        StartCoroutine(FlashColor());

        if(health <= 0)
        {
            int randomNumber = Random.Range(0, 101); //0 and 100, last number is ingored for random.range
            if (randomNumber < pickupChance)
            {
                SpawnWeaponPickup();
            }

            int randHealth = Random.Range(0, 101);
            if(randHealth  < healthPickupChance)
            {
                SpawnHealthPickup();
            }
            
            Death();
        }
    }

    IEnumerator FlashColor()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.color = Color.red;
        }

        yield return new WaitForSeconds(0.25f);
        
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.color = Color.white;
        }

        yield return null;
    }

    private void SpawnWeaponPickup()
    {
        GameObject randomPickup = pickups[Random.Range(0, pickups.Length)]; //chooses an random pickup from the list of pickups
        Instantiate(randomPickup, m_transform.position, m_transform.rotation);

    }

    private void SpawnHealthPickup()
    {
        Instantiate(healthPickup, m_transform.position, m_transform.rotation);
    }

    public void Death()
    {
        Instantiate(enemyDeathEffect, m_transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

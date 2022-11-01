using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    public int health;

    private float halfHealth;
    private Animator anim;
    private UIManager uiManager;
    private Slider healthBar;

    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public Transform m_transform;
    [HideInInspector]
    public Transform playerAttackPoint; //the point on the character model for the enemy to attack

    [Tooltip("If the value is -1, wont happen")]
    public int pickupChance; //between 0 and 100, the chance a weapon spawns
        [Tooltip("If the value is -1, wont happen")]
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

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float fleeSpeed;
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

        halfHealth = health / 2;
        anim = this.GetComponentInChildren<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        healthBar = uiManager.bossHPBar;
        healthBar.gameObject.SetActive(true);
        healthBar.maxValue = health;
        healthBar.value = health;
    }


    protected virtual void Update()
    {
        //adding safty check while we port all ai over to the new system
        if (currentState != null)
            currentState.Tick();
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


    public void TakeDamage(int amount)
    {

        // m_Timer.ReduceCurrentTime(); //boss will attack sooner when it takes damage
        health -= amount;

        healthBar.value = health;
        StartCoroutine(FlashColor());

        if (health <= 0)
        {
            int randomNumber = Random.Range(0, 101); //0 and 100, last number is ingored for random.range
            if (randomNumber < pickupChance)
            {
                SpawnWeaponPickup();
            }

            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {
                SpawnHealthPickup();
            }

            Die();
        }
        //anything else we want to happen anytime the boss takes damage
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


    public void Die()
    {
        healthBar.gameObject.SetActive(false);
        Instantiate(enemyDeathEffect, m_transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}

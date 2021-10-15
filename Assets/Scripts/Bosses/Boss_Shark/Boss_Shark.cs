using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script that holds the default boss info
public class Boss_Shark : MonoBehaviour
{
    public int health;

    private float halfHealth;
    private Animator anim;
    private UIManager uiManager;
    private Slider healthBar;

    private void Start()
    {
        halfHealth = health / 2;
        anim = this.GetComponentInChildren<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        healthBar = uiManager.bossHPBar;
        healthBar.gameObject.SetActive(true);
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.value = health;
       // m_Timer.ReduceCurrentTime(); //boss will attack sooner when it takes damage
        if (health <= 0)
        {
            Die();

        }

        //anything else we want to happen anytime the boss takes damage
    }

    public void Die()
    {
        healthBar.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}

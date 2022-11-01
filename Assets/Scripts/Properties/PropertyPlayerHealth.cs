using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyPlayerHealth : MonoBehaviour
{
    public int health = 0;
    //Health UI
    public Image[] healthGears;
    public Sprite emptyGear;
    public Sprite fullGear;
    //visual effect
    [Tooltip("Attach hurt pannel from the canvas here")]
    public Animator hurtPanel;

    private void Start()
    {
        UpdateHealthUI(health);
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthGears.Length; i++)  //for each possible heart
        {
            if (i < currentHealth)  //if our great index is less than current health fill the heart, do this untill it is not less
            {
                healthGears[i].sprite = fullGear;
            }
            else
            {
                healthGears[i].sprite = emptyGear; //turn the rest of the hearts empty
            }
        }
    }

    public void Heal(int healAmmount)
    {
        if (health + healAmmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmmount;
        }

        UpdateHealthUI(health);
    }


    public void TakeDamage(int damageAmmount)
    {
        health -= damageAmmount;
        UpdateHealthUI(health); //updates the health ui
        HurtPanel(); //updates the hurt panel
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<SceneTransitions>().LoadScene("LoseScene");
        //Destroy(this.gameObject);
    }


    private void HurtPanel()
    {
        hurtPanel.SetTrigger("hurt");
    }
}

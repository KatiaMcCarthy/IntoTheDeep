using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEnemy : Enemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPostion;
    private Animator m_anim;

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;

    public float meleeAttackSpeed;
    public float stopDistance;
    private float attackTime;

    public override void Start() //instead of running enemy start fuction run this one instead
    {
        base.Start(); //calls start function from base class
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetPostion = new Vector2(randomX, randomY);
        m_anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(player != null)
        {
            Debug.Log("Player isnt null");
            if(Vector2.Distance(m_transform.position, targetPostion) > 0.5f)
            {
                Debug.DrawLine(m_transform.position, targetPostion);

                float AngleRad = Mathf.Atan2(targetPostion.y - m_transform.position.y, targetPostion.x - m_transform.position.x);
                float angle = (180 / Mathf.PI) * AngleRad;

                m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                m_transform.position = Vector2.MoveTowards(m_transform.position, targetPostion, speed * Time.deltaTime);
                m_anim.SetBool("isRunning", true);
            }else
            {

                float AngleRad = Mathf.Atan2(player.position.y - m_transform.position.y, player.position.x - m_transform.position.x);
                float angle = (180 / Mathf.PI) * AngleRad;

                m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);


                m_anim.SetBool("isRunning", false);

                if(Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    m_anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(m_transform.position, player.position) < stopDistance)
            {
                //rotate towards the player
                float AngleRad = Mathf.Atan2(playerAttackPoint.position.y - m_transform.position.y, playerAttackPoint.position.x - m_transform.position.x);
                float angle = (180 / Mathf.PI) * AngleRad;

                m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                if (Time.time >= attackTime)
                    {
           
                        attackTime = Time.time + timeBetweenAttacks;
                        StartCoroutine(MeleeAttack());
                     }
                
            }
        }
    }

    public void Summon()
    {
        if(player != null)
        {
            Instantiate(enemyToSummon, m_transform.position, m_transform.rotation);
        }
    }

    IEnumerator MeleeAttack() //a coroutine that will execute once triggered, allows for you to have an animation playing over frames, were as if  you did in update it would restart every frame
    {
        player.GetComponent<PlayerScript>().TakeDamage(damage);

        //animation for leaping at the player
        Vector2 originalPosition = m_transform.position; // the position before he leeps to the player
        Vector2 targetPosition = playerAttackPoint.transform.position; // the position of the player before he leeps the the player
        Debug.Log("hit corroutine");
        float percent = 0; // stores how much of the animation we have done so far
        while (percent <= 1)
        {
            Debug.Log("hit while");
            percent += Time.deltaTime * meleeAttackSpeed; //this will incriment the animation bit by bit, the factor of attack speed allows you to control how fast the animation plays
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4; //allows you to move towards the player, the mathf.pow turns it into a parabellic curve, the negitave sets the direction of the curve. (in this case flips it from U to n); -4x^2 + 4x; This is the rate that it moves, the rate is bouncing from .36 at x = .1 to 1 at x = .5, to .36 at x = .9. CONCERNING LERPING When t = 0 returns a. When t = 1 return b. When t = 0.5 returns the midpoint of a and b.

            m_transform.position = Vector2.Lerp(originalPosition, targetPosition, formula); //we move from origninal postion to target, by rate of formula
            yield return null; //lets you run the animation over a period of time, by allowing the animation to percist over frames
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;
    public float attackSpeed;

    private float attackTime;


    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(m_transform.position, playerAttackPoint.position) > stopDistance)
            {
                Debug.DrawLine(m_transform.position, playerAttackPoint.position);

                float AngleRad = Mathf.Atan2(playerAttackPoint.position.y - m_transform.position.y, playerAttackPoint.position.x - m_transform.position.x);
                float angle = (180 / Mathf.PI) * AngleRad;

                m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                m_transform.position = Vector2.MoveTowards(m_transform.position, player.position, speed * Time.deltaTime);


            }
            else
            {
                Debug.Log("In range to attack");
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(MeleeAttack());

                }
            }

        }
    }


    IEnumerator MeleeAttack()
    {
        ps.TakeDamage(damage);

        //animation for leaping at the player
        Vector2 originalPosition = m_transform.position; // the position before he leeps to the player
        Vector2 targetPosition = playerAttackPoint.transform.position; // the position of the player before he leeps the the player
        Debug.Log("hit corroutine");
        float percent = 0; // stores how much of the animation we have done so far
        while (percent <= 1)
        {
            Debug.Log("hit while");
            percent += Time.deltaTime * attackSpeed; //this will incriment the animation bit by bit, the factor of attack speed allows you to control how fast the animation plays
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

            if (formula >= 0.8f)
            {
                ps.b_Hit = true;
            }
            else
            {
                ps.b_Hit = false;
            }


            m_transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null; //lets you run the animation over a period of time, by allowing the animation to percist over frames
        }

    }



}
    
    
    
    
    
    
    //IEnumerator MeleeAttack() //a coroutine that will execute once triggered, allows for you to have an animation playing over frames, were as if  you did in update it would restart every frame
    //{
    //    ps.TakeDamage(damage);

    //    //animation for leaping at the player
    //    Vector2 originalPosition = m_transform.position; // the position before he leeps to the player
    //    Vector2 targetPosition = playerAttackPoint.transform.position; // the position of the player before he leeps the the player
    //    Debug.Log("hit corroutine");
    //    float percent = 0; // stores how much of the animation we have done so far
    //    while (percent <= 1)
    //    {
    //        Debug.Log("hit while");
    //        percent += Time.deltaTime * attackSpeed; //this will incriment the animation bit by bit, the factor of attack speed allows you to control how fast the animation plays
    //        float formula = (-Mathf.Pow(percent, 2) + percent) * 4; //allows you to move towards the player, the mathf.pow turns it into a parabellic curve, the negitave sets the direction of the curve. (in this case flips it from U to n); -4x^2 + 4x; This is the rate that it moves, the rate is bouncing from .36 at x = .1 to 1 at x = .5, to .36 at x = .9. CONCERNING LERPING When t = 0 returns a. When t = 1 return b. When t = 0.5 returns the midpoint of a and b.

    //        if (formula >= 0.8f)
    //        {
    //            ps.b_Hit = true;
    //        }
    //        else
    //        {
    //            ps.b_Hit = false;
    //        }


    //        m_transform.position = Vector2.Lerp(originalPosition, targetPosition, formula); //we move from origninal postion to target, by rate of formula
    //        yield return null; //lets you run the animation over a period of time, by allowing the animation to percist over frames
    //    }
    //}


//}














   

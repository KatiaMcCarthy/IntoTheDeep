using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//todo, take this over to attack behaviour, currently not patroling setup
public class SharkAttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private LayerMask enemiesToHit;

    private float attackTime;

    private SharkBoss boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<SharkBoss>();
        boss.OnAttackCall += DoAttack;
    }

    void DoAttack()
    {
        if (Time.time >= attackTime) //if we have the cooldown and the the button pressed
        {
            attackTime = Time.time + attackSpeed;

            Attack();
        }

    }

    private void Attack()
    {
        //create a cirlce, overlap all on the player tag, boss will have its own tag
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemiesToHit);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if(enemiesToDamage[i].GetComponent<PropertyPlayerHealth>() != null)
            enemiesToDamage[i].GetComponent<PropertyPlayerHealth>().TakeDamage(damage);
            else if(enemiesToDamage[i].GetComponent<Enemy>()!= null)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        SharkBrain brain = boss.GetComponent<SharkBrain>();
        brain.ResetBrain();
        brain.patrolWeight = 20.0f;
        brain.chaseWeight = 80.0f;
        brain.fleeWeight = 90.0f;

        boss.GetComponent<SharkBrain>().DecideState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    private void OnDestroy()
    {
        boss.OnAttackCall -= DoAttack;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkRangedBehaviour : MonoBehaviour
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
    private GameObject projectile;

    private float attackTime;

    private SharkBoss boss;
    
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<SharkBoss>();
        boss.OnRangedAttackCall += DoAttack;
    }

    void DoAttack()
    {
        if (boss.player != null)
        {
            float AngleRad = Mathf.Atan2(boss.playerAttackPoint.transform.position.y - transform.position.y, boss.playerAttackPoint.transform.position.x - transform.position.x);
            float angle = (180 / Mathf.PI) * AngleRad;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
 
            if (boss.player != null && Vector2.Distance(boss.m_transform.position, boss.playerAttackPoint.position) < attackRange)
            {
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + attackSpeed;

                     //can call animation wich calles the attack
                    RangedAttack();
                }
            }
            
            if(Vector2.Distance(transform.position, boss.GetPlayer().position) >= GetAttackRange())
            {
                RecalcState();
            }
        }
    }

    public void RangedAttack()
    {
        if (boss.player != null)
        {
            Vector2 direction = boss.player.position - attackPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            attackPoint.rotation = rotation;

            Instantiate(projectile, attackPoint.position, attackPoint.rotation);
        }
    }

    private void RecalcState()
    {
        SharkBrain brain = boss.GetComponent<SharkBrain>();
        brain.ResetBrain();
        brain.chaseWeight = 100.0f;

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
        boss.OnRangedAttackCall -= DoAttack;
    }
}



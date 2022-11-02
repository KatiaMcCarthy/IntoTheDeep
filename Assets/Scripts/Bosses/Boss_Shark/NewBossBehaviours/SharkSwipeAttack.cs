using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSwipeAttack : MonoBehaviour
{
    private SharkBoss boss;
    [SerializeField] private int damage;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask enemiesToHit;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<SharkBoss>();
        animator = GetComponent<Animator>();
        boss.OnSwipeAttackCall += PlayAnimation;
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("Swipe");
    }

    //called from animaition, mid animatoin
    public void Swipe()
    {
        //create a cirlce, overlap all on the player tag, boss will have its own tag
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemiesToHit);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].GetComponent<PropertyPlayerHealth>() != null)
                enemiesToDamage[i].GetComponent<PropertyPlayerHealth>().TakeDamage(damage);
            else if (enemiesToDamage[i].GetComponent<Enemy>() != null)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    //called in animation at end of state
    private void HandleState()
    {
        //change the state
        SharkBrain brain = boss.GetComponent<SharkBrain>();
        brain.ResetBrain();
        brain.fleeWeight = 30.0f;
        brain.chaseWeight = 10.0f;

        boss.GetComponent<SharkBrain>().DecideState();
    }

    private void OnDestroy()
    {
        boss.OnChargeAttackCall -= PlayAnimation;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);        
    }
}

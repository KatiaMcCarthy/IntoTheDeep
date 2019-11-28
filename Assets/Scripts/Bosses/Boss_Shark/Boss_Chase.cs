using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chase : StateMachineBehaviour
{
    private GameObject player;
    public float speed;
    private Transform parent;

    [HideInInspector]
    public Transform playerAttackPoint; //the point on the character model for the enemy to attack

    public float stopDistance;
    public float timeBetweenAttacks;
    private float attackTime;
    private Boss_Attack ba;

    public bool b_BossCanAttack = true;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
        parent = animator.transform.parent;
        ba = parent.GetComponent<Boss_Attack>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackPoint = GameObject.FindGameObjectWithTag("PlayerAttackPoint").transform;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player!=null)
        {
            if (Vector2.Distance(parent.position, playerAttackPoint.position) > stopDistance)
            {
                Debug.DrawLine(parent.position, playerAttackPoint.position);

                float AngleRad = Mathf.Atan2(playerAttackPoint.position.y - parent.position.y, playerAttackPoint.position.x - parent.position.x);
                float angle = (180 / Mathf.PI) * AngleRad;

                parent.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                parent.position = Vector2.MoveTowards(parent.position, player.transform.position, speed * Time.deltaTime);


            }
            else
            {
                if (b_BossCanAttack && (Time.time >= attackTime))
                {
                    attackTime = Time.time + timeBetweenAttacks;
                    Debug.Log("Attacked");

                    animator.SetBool("isAttacking", true);

                    //ba.Attack();
                }
                else
                {
                    animator.SetBool("isAttacking", false);
                }

            }
        }

    }


    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

   
}

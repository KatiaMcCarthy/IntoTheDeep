using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Patrol : StateMachineBehaviour
{
    public int radius;
    private Vector2 randomPoint = Vector2.zero;
    public float speed;
    private Transform parent;

    private Vector2 lastRandPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // the start function for the animation, happens at the start of the animation
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parent = animator.transform.parent;
        if (randomPoint == Vector2.zero)
        {
            randomPoint = Random.insideUnitCircle * radius;  //random point in a circle of radius - radius, this circle is drawn from 0,0 in world
         
        }else
        {
            randomPoint = lastRandPoint;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // called each frame during the animation
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawLine(animator.transform.position, randomPoint);
        parent.transform.position = Vector2.MoveTowards(parent.transform.position, randomPoint, speed * Time.deltaTime);

        if(Vector2.Distance(parent.transform.position, randomPoint) < 0.1f)
        {
            randomPoint = Random.insideUnitCircle * radius;  //random point in a circle of radius - radius, this circle is drawn from 0,0 in world
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("stage2");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // called once right before exiting the animation
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lastRandPoint = randomPoint;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Patrol : StateMachineBehaviour
{
    public int radius;
    private Vector2 randomPoint = Vector2.zero;
    public float speed;
    private Transform parent;
    private Vector2 parentPos;

    private Vector2 lastRandPoint;

    private Vector2 randPoint;
    public GameObject backgroundColliderObject;
    public Collider2D levelCollider;

    public bool b_bossAttackTrigger = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // the start function for the animation, happens at the start of the animation
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parent = animator.transform.parent;
        parentPos = parent.position;

        backgroundColliderObject = GameObject.FindGameObjectWithTag("backgroundCollider");
        levelCollider = backgroundColliderObject.GetComponent<PolygonCollider2D>();

        randPoint = GetRandomPointInCollider();  //finds a random point in the collider
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // called each frame during the animation
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parentPos = parent.position;

        if (randPoint != null)
        {
         
            Debug.DrawLine(animator.transform.position, randPoint);

            float AngleRad = Mathf.Atan2(randPoint.y - parent.position.y, randPoint.x - parent.position.x);
            float angle = (180 / Mathf.PI) * AngleRad;

            parent.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            parent.transform.position = Vector2.MoveTowards(parent.transform.position, randPoint, speed * Time.deltaTime);

            if (Vector2.Distance(parent.transform.position, randPoint) < 0.1f)
            {
                randPoint = GetRandomPointInCollider();
            }

        }

        if(b_bossAttackTrigger == true)
        {
            animator.SetTrigger("stage2"); //this triggers the chase behaviour
            b_bossAttackTrigger = false; //resets the trigger
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // called once right before exiting the animation
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //lastRandPoint = randomPoint;
    }

    Vector2 GetRandomPointInCollider()
    {
        Vector2 point = new Vector2(
        Random.Range(levelCollider.bounds.min.x, levelCollider.bounds.max.x),
        Random.Range(levelCollider.bounds.min.y, levelCollider.bounds.max.y)
    );

        if (point != levelCollider.ClosestPoint(point))
        {
            Debug.Log("Out of the collider! Looking for the other point...");
            point = GetRandomPointInCollider();  //cycles through again to see if the point exists
        } 

        return point;
    }
}

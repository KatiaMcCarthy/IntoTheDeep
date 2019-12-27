using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FleeAnim : StateMachineBehaviour
{
    private Transform parent;
    private Vector2 parentPos;

    private Vector2 randPoint;
    public GameObject backgroundColliderObject;
    public Collider2D levelCollider;

    public float fleeDistance;

    public float speed;

    //here we need the boss to flee from the player for x ammmount of time? patrol for x time?

    // this is where the reset it?
    // go to a random point, with a maximum distance of X then go to chase behaviour?



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parent = animator.transform.parent;
        parentPos = parent.position;

        backgroundColliderObject = GameObject.FindGameObjectWithTag("backgroundCollider");
        levelCollider = backgroundColliderObject.GetComponent<PolygonCollider2D>();

        randPoint = GetRandomPointInColliderAwayFromPlayer();  //finds a random point in the collider

        Debug.Log(randPoint);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parentPos = parent.position;

        if (randPoint != null)
        {
            Debug.DrawLine(animator.transform.position, randPoint);

            float AngleRad = Mathf.Atan2(randPoint.y - parent.position.y, randPoint.x - parent.position.x);
            float angle = (180 / Mathf.PI) * AngleRad;

            parent.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            parent.transform.position = Vector2.MoveTowards(parent.transform.position, randPoint, speed * Time.deltaTime);  //moves us to the point

            if (Vector2.Distance(parent.transform.position, randPoint) < 0.1f)
            {
                animator.SetBool("isFleeing", false);  //this redirects us to the chase behavior

            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

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

    Vector2 GetRandomPointInColliderAwayFromPlayer()
    {
        Vector2 point = GetRandomPointInCollider();

        if(Vector2.Distance(point, parentPos) <= fleeDistance)
        {
            point = GetRandomPointInColliderAwayFromPlayer();
        }

        return point;
    }
}

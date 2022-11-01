using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPatrolBehaviour : MonoBehaviour
{
    [SerializeField]
    protected Collider2D levelCollider;
    private Vector2 randPoint;
    private SharkBoss boss;
    [SerializeField] private float attackRange;

    private void Start()
    {
        boss = GetComponent<SharkBoss>();
        levelCollider = GameObject.FindGameObjectWithTag("backgroundCollider").GetComponent<Collider2D>();

        randPoint = GetRandomPointInCollider();
        boss.OnMoveCall += MoveToward;
    }

    private void MoveToward(float speed)
    {
        if (randPoint != null)
        {

            Debug.DrawLine(transform.position, randPoint);

            float AngleRad = Mathf.Atan2(randPoint.y - transform.position.y, randPoint.x - transform.position.x);
            float angle = (180 / Mathf.PI) * AngleRad;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            transform.transform.position = Vector2.MoveTowards(transform.position, randPoint, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, randPoint) < 0.2f)
            {
                randPoint = GetRandomPointInCollider();
            }

        }

        if (Vector3.Distance(transform.position,boss.GetPlayer().position) <= attackRange)
        {

            SharkBrain brain = boss.GetComponent<SharkBrain>();
            brain.ResetBrain();
            brain.attackWeight = 15.0f;
            brain.rangedWeight = 30.0f;
            boss.GetComponent<SharkBrain>().DecideState();

            GetComponent<SharkBrain>().DecideState();
        }
    }

    private Vector2 GetRandomPointInCollider()
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


    private void OnDestroy()
    {
        boss.OnMoveCall -= MoveToward;
    }
}

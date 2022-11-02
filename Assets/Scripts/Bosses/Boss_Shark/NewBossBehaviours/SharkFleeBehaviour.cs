using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkFleeBehaviour : MonoBehaviour
{

    // want to select a point X units away from the player in the oposite direction that is within the collider?
    // we want to move in the negitive vector of the player

    // get direction via b - a, get negitive direction by a - b

    [SerializeField]
    protected Collider2D levelCollider;

    private Vector2 destination;
    private SharkBoss boss;
    private PlayerScript ps;

    [SerializeField] private float fleeDistance;


    private void Start()
    {
        boss = GetComponent<SharkBoss>();
        ps = FindObjectOfType<PlayerScript>();
        levelCollider = GameObject.FindGameObjectWithTag("backgroundCollider").GetComponent<Collider2D>();
        boss.OnFleeCall += MoveToward;
       


    }

    private void MoveToward(float speed)
    {

        //state changes if we ever get 10 units away of the player
        if (Vector2.Distance(transform.position, ps.transform.position) < fleeDistance)
        destination = GetDestination();

        if (destination != null)
        {
            Debug.DrawLine(transform.position, destination);

            float AngleRad = Mathf.Atan2(destination.y - transform.position.y, destination.x - transform.position.x);
            float angle = (180 / Mathf.PI) * AngleRad;

            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            transform.transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < 0.2f)
            {
                SharkBrain brain = boss.GetComponent<SharkBrain>();
                brain.ResetBrain();
                brain.patrolWeight = 100.0f;

                boss.GetComponent<SharkBrain>().DecideState();
            }
        }
    }

    private Vector2 GetDestination()
    {

        /// if we have destination = ps.trasform.position, we move to the player, we want same magnitued as that, but the - direction
        
            Vector3 dirToPlayer = transform.position - ps.transform.position;
            Vector2 newPos = transform.position + dirToPlayer;

        return newPos;

        //need to add on if it is inside of level collider


    }

    private void OnDestroy()
    {
        boss.OnFleeCall -= MoveToward;
    }
}
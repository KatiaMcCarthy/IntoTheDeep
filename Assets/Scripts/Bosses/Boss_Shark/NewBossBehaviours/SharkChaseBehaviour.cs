using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkChaseBehaviour : MonoBehaviour
{
    private Vector2 destination;
    private SharkBoss boss;
    private PlayerScript ps;

    private void Start()
    {
        boss = GetComponent<SharkBoss>();
        ps = FindObjectOfType<PlayerScript>();
        boss.OnChaseCall += MoveToward;

    }

    private void MoveToward(float speed)
    {
        destination = ps.transform.position;

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
                brain.attackWeight = 30.0f;   //30%
                brain.chargeLungeWeight = 70.0f; //40%

                boss.GetComponent<SharkBrain>().DecideState();
            }
        }
    }

    private void OnDestroy()
    {
        boss.OnChaseCall -= MoveToward;
    }
}
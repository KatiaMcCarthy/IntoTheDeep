using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private PlayerScript playerScript;
    private Vector2 targetPosition;
    private Transform m_Transform;

    public float speed = 0.0f;
    public int damage;

    public GameObject effect;

    private void Start()
    {
        m_Transform = this.transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(m_Transform.position, targetPosition) > 0.1f)
        {
            Debug.DrawLine(m_Transform.position, targetPosition);
            m_Transform.position = Vector2.MoveTowards(m_Transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            Death();
        }
    }

    private void Death()
    {
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

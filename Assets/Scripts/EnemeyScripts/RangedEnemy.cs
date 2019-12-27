using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;

    private float attackTime;
    private Animator m_anim;

    public Transform shotPoint;
    public GameObject enemyBullet;

    public override void Start()
    {
        base.Start();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(m_transform.position, player.position) > stopDistance)
            {
                Debug.DrawLine(m_transform.position, player.position);

                float AngleRad = Mathf.Atan2(playerAttackPoint.position.y - m_transform.position.y, playerAttackPoint.position.x - m_transform.position.x);
                float angle = (180 / Mathf.PI) * AngleRad;

                m_transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                m_transform.position = Vector2.MoveTowards(m_transform.position, player.position, speed * Time.deltaTime);
            }


            if (Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttacks;
                m_anim.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack()
    {
        if (player != null)
        {
            Vector2 direction = player.position - shotPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            shotPoint.rotation = rotation;

            Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
        }
    }
}

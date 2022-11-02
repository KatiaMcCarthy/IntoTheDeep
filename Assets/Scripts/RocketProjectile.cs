using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{

    private Animator m_Anim;
    public float frequency = 20.0f; //speed of sine movement
    public float magnitude = 0.5f; //size of sine movment

    private Vector3 axis;
    private Vector3 pos;
    public GameObject lightFlare;

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerScript>();
        pRB = player.GetComponent<Rigidbody2D>();

        playerSpeed = pRB.velocity.magnitude;
        speed = speed + playerSpeed;

        pos = transform.position;
        axis = transform.right;
        Invoke("DestroyProjectile", lifeTime);

    }

    public override void Update()
    {
        pos += transform.up * Time.deltaTime * speed;

        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        //want to move differntly, on a sin wave

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);

        // If it hits something...
        if (hit != false)
        {

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                DestroyProjectile();
            }
            else
            {
                //do nothing
            }
        }
    }

    public override void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(lightFlare, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

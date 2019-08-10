using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;  //speed cant be low
    public float lifeTime = 0.0f;
    public int damage = 0;

    public GameObject explosion;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerScript ps;
    [HideInInspector]
    public Rigidbody2D pRB;
    [HideInInspector]
    public float playerSpeed;


    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerScript>();
        pRB = player.GetComponent<Rigidbody2D>();

        playerSpeed = pRB.velocity.magnitude;
        speed = speed + playerSpeed;

        Invoke("DestroyProjectile", lifeTime);  
    }
    // Update is called once per frame
    public virtual void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);

        // If it hits something...
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

    public virtual void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}

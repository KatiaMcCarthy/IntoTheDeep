using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolingShallow : MonoBehaviour
{
    [HideInInspector]
    public Transform m_transform;

    // movement target controls

    private Vector2 randPoint;
    public GameObject insideColliderObject;
    public Collider2D insideCollider;

    public GameObject outsideColliderObject;
    public Collider2D outsideCollider;

    private float getPointTime;
    //how often you look for a new point
    public float timeBetweenPoints;
    //stagger ammount
    private float staggerAmmount;

    public float speed;

    public Sprite[] images;
    private SpriteRenderer sr;
    private Transform artTrans;

    public bool melleEnemy = false;
    public bool rangedEnemy = false;
    public bool summonerEnemy = false;
    public bool minionEnemy = false;
    public bool bossEnemy = false;

    private int rotationAmmount = 90;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.gameObject.transform;
        outsideColliderObject = GameObject.FindGameObjectWithTag("outsideCollider");
        outsideCollider = outsideColliderObject.GetComponent<BoxCollider2D>();
        insideColliderObject = GameObject.FindGameObjectWithTag("insideCollider");
        insideCollider = insideColliderObject.GetComponent<BoxCollider2D>();

        randPoint = GetRandomPointInCollider();  //finds a random point in the collider

        sr = GetComponentInChildren<SpriteRenderer>();
        artTrans = GetComponentInChildren<Transform>();

        staggerAmmount = Random.Range(0.1f, 1.5f);
        timeBetweenPoints = timeBetweenPoints - staggerAmmount;

        if (melleEnemy == true)
        {
            sr.sprite = images[0];
            artTrans.localScale = new Vector3(5f, 5f, 1);
            rotationAmmount = -90;
        }

        if (rangedEnemy == true)
        {
            sr.sprite = images[1];
            artTrans.localScale = new Vector3(5f, 5f, 1);
            rotationAmmount = 90;
        }

        if (summonerEnemy == true)
        {
            sr.sprite = images[2];
            artTrans.localScale = new Vector3(5f, 5f, 1);
            rotationAmmount = -90;
        }

        if (minionEnemy == true)
        {
            sr.sprite = images[3];
            artTrans.localScale = new Vector3(6.5f, 6.5f, 1);
            rotationAmmount = 90;
        }

        if (bossEnemy == true)
        {
            sr.sprite = images[4];
            artTrans.localScale = new Vector3(0.85f, 0.85f, 1);
            rotationAmmount = 90;
        }
        // we may need to affect the scale of your child object


    }

    // Update is called once per frame
    void Update()
    {
        //TODO: need to modify the -90 value dependant on what type of art is assigned

        if (randPoint != null)
        {

            Debug.DrawLine(m_transform.position, randPoint);

            float AngleRad = Mathf.Atan2(randPoint.y - m_transform.position.y, randPoint.x - m_transform.position.x);
            float angle = (180 / Mathf.PI) * AngleRad;

            m_transform.rotation = Quaternion.AngleAxis(angle - rotationAmmount, Vector3.forward);

            m_transform.transform.position = Vector2.MoveTowards(m_transform.transform.position, randPoint, speed * Time.deltaTime);

            // if (Vector2.Distance(m_transform.transform.position, randPoint) < 0.1f)
            if (Time.time >= getPointTime)
            {
                getPointTime = Time.time + timeBetweenPoints;

                randPoint = GetRandomPointInCollider();
            }
            


        }
    }

    //calculating the random point
    Vector2 GetRandomPointInCollider()
    {
        Vector2 point = new Vector2(
        Random.Range(outsideCollider.bounds.min.x, outsideCollider.bounds.max.x),
        Random.Range(outsideCollider.bounds.min.y, outsideCollider.bounds.max.y)
    );
        //if the point is outside of the outside collider, get a new one
        if (point != outsideCollider.ClosestPoint(point))
        {
            Debug.Log("Out of the collider! Looking for the other point...");
            point = GetRandomPointInCollider();  //cycles through again to see if the point exists
        }

        //if the point is inside of the inner collider, get a new one
        if (IsPointWithinCollider(insideCollider, point))
        {
            point = GetRandomPointInCollider();
        }
        
        //if the point isnt far away, select a new one
        if(Vector2.Distance(this.transform.position, point) < 5)
        {
            point = GetRandomPointInCollider();
        }

        return point;
    }

    public static bool IsPointWithinCollider(Collider2D collider, Vector3 point)
    {
        Vector3 closest = collider.ClosestPoint(point);
        // Because closest=point if point is inside - not clear from docs I feel
        return closest == point;
    }
}

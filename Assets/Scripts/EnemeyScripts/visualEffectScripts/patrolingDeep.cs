using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolingDeep : MonoBehaviour
{
    [HideInInspector]
    public Transform m_transform;

    private Vector2 randPoint;
    public GameObject backgroundColliderObject;
    public Collider2D levelCollider;

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
        backgroundColliderObject = GameObject.FindGameObjectWithTag("backgroundCollider");
        levelCollider = backgroundColliderObject.GetComponent<PolygonCollider2D>();

        randPoint = GetRandomPointInCollider();  //finds a random point in the collider

        sr = GetComponentInChildren<SpriteRenderer>();
        artTrans = GetComponentInChildren<Transform>();

        if(melleEnemy == true)
        {
            sr.sprite = images[0];
            artTrans.localScale = new Vector3(0.5f, 0.5f, 1);
            rotationAmmount = -90;
        }

        if(rangedEnemy == true)
        {
            sr.sprite = images[1];
            artTrans.localScale = new Vector3(0.5f, 0.5f, 1);
            rotationAmmount = 90;
        }

        if(summonerEnemy == true)
        {
            sr.sprite = images[2];
            artTrans.localScale = new Vector3(0.45f, 0.45f, 1);
            rotationAmmount = 90;
        }

        if(minionEnemy == true)
        {
            sr.sprite = images[3];
            artTrans.localScale = new Vector3(0.65f, 0.65f, 1);
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

            m_transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            m_transform.transform.position = Vector2.MoveTowards(m_transform.transform.position, randPoint, speed * Time.deltaTime);

            if (Vector2.Distance(m_transform.transform.position, randPoint) < 0.1f)
            {
                randPoint = GetRandomPointInCollider();
            }

        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public GameObject[] shotPoints;
    public float timeBetweenShots;

    private float shotTime;
    private Transform m_transform;

    // Start is called before the first frame update
    void Awake()
    {
        m_transform = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //this is the angle that the weapon must rotate around to face the cursor
        Quaternion rotation = Quaternion.AngleAxis(angle -90, Vector3.forward); //Vector 3.forward is z axis
        m_transform.rotation = rotation;

        if(shotPoints == null)
        {
            shotPoints = GameObject.FindGameObjectsWithTag("ShotPoint");
        }


        if(Input.GetMouseButton(0))
        {
            if(Time.time >= shotTime)
            {
                for (int i = 0; i < shotPoints.Length; i++)
                {
                    Instantiate(projectile, shotPoints[i].transform.position, shotPoints[i].transform.rotation);
                }
                shotTime = Time.time + timeBetweenShots; //sets the fire rate
            }
        }
    }
}

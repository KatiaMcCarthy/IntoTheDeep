using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector3 offset;



    private Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        offset = transform.position - playerTransform.position;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerTransform != null)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x + offset.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y + offset.y, minY, maxY);

            m_transform.position = Vector3.Lerp(m_transform.position, new Vector3 (clampedX, clampedY, playerTransform.position.z + offset.z) , speed);
        }
    }
}

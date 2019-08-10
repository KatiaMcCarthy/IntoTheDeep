using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{

    public float speed = 0.0f;
    public float slowFactor = 0.0f;
    public int health = 0;
    public float turnFactor = 0.0f;

    //Health UI
    public Image[] healthGears;
    public Sprite emptyGear;
    public Sprite fullGear;

    private Rigidbody2D m_rb;
    private Vector2 moveAmmount;
    private Animator m_anim;
    private Camera m_cam;
    private Transform m_transform;
    private Vector2 moveInput;

    MoveDirection m_MoveDirection;
    enum MoveRotation { None,Left, Right};
    MoveRotation m_MoveRotation;

    Vector2 m_ResetVector;
    Vector2 m_UpVector;
    Vector2 m_RightVector;
    Vector2 m_StartPosition, m_StartForce;

    private Transform weaponParentObject;
    private Transform weaponLocationPt;

    public ParticleSystem p_Bubbles;
    public ParticleSystem.MainModule p_BubblesMain;
    private Quaternion lastRot; // holds the last rotation of the player;


    [HideInInspector]
    public bool b_Hit = false; //bool indentifying if the player was hit 

    /// <summary>
    /// section for camera shake
    /// </summary>
    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;


    //visual effect
    public Animator hurtPanel;

    private AbilityFlare m_AF;

    // Start is called before the first frame update
    private void Start()
    {
        m_transform = this.gameObject.GetComponent<Transform>();
        m_cam = Camera.main;
        m_anim = this.gameObject.GetComponent<Animator>();
        m_rb = this.gameObject.GetComponent<Rigidbody2D>();
        weaponParentObject = GameObject.FindGameObjectWithTag("WeaponParent").transform;
        weaponLocationPt = GameObject.FindGameObjectWithTag("WeaponLocationPt").transform;
        p_Bubbles = GameObject.FindGameObjectWithTag("PlayerBubbles").GetComponent<ParticleSystem>();
        //Initalization for bubbles
        //p_BubblesMain = p_Bubbles.main;
       // p_BubblesMain.loop = false;

        //This starts with the Rigidbody not moving in any direction at all
        m_MoveDirection = MoveDirection.None;

        //These are the GameObject’s starting position and Rigidbody position
        m_StartPosition = transform.position;
        m_StartForce = m_rb.transform.position;

        //This Vector is set to 1 in the y axis (for moving upwards)
        m_UpVector = Vector2.up;
        //This Vector is set to 1 in the x axis (for moving in the right direction)
        m_RightVector = Vector2.right;
        //This Vector is zeroed out for when the Rigidbody should not move
        m_ResetVector = Vector2.zero;

        UpdateHealthUI(health); //intializes the health ui

        if (VirtualCamera != null) //intializes camera componenet
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        lastRot = Quaternion.identity; //intializes the last rotiaton
        hurtPanel = GameObject.FindGameObjectWithTag("hurtPanel").GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        m_UpVector = m_rb.transform.up;
        m_RightVector = m_rb.transform.right;


        #region OtherMovement
        if (Input.GetKey(KeyCode.W))
        {
            m_MoveDirection = MoveDirection.Up;
            if (p_Bubbles.isStopped)
            {
                p_Bubbles.Play();
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_MoveDirection = MoveDirection.Down;
            if (p_Bubbles.isStopped)
            {
                p_Bubbles.Play();
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_MoveRotation = MoveRotation.Left;
           //bIsMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_MoveRotation = MoveRotation.Right;
           // bIsMoving = true;
        }
        #endregion OtherMovement

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            m_MoveDirection = MoveDirection.None;
            m_MoveRotation = MoveRotation.None;
        }

        //Visual Effects

        //camera shake control
        if(b_Hit == true)
        {
            ShakeElapsedTime = ShakeDuration;

        }


        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }


        //partical trail control
        lastRot = transform.rotation;

        if (m_rb.velocity == Vector2.zero && ((transform.rotation.y != lastRot.y && transform.rotation.y - lastRot.y >= 0) || (transform.rotation.y != lastRot.y && transform.rotation.y - lastRot.y <= 0)))
        {
            //if we are stopped
            p_Bubbles.Stop(); //stop the animation
            p_Bubbles.Clear();
        }

        //animation control
        if (m_MoveDirection != MoveDirection.None)
        {
            m_anim.SetBool("isRunning", true);
        }
        else
        {
            m_anim.SetBool("isRunning", false);
        }



    }

    private void FixedUpdate()
    {
        // m_rb.MovePosition(m_rb.position + moveAmmount * Time.fixedDeltaTime);

        if(m_MoveRotation == MoveRotation.None)
        {
            m_rb.MoveRotation(m_rb.rotation);
        }
        if(m_MoveRotation == MoveRotation.Left)
        {
            m_rb.MoveRotation(m_rb.rotation + turnFactor * 12 * Time.fixedDeltaTime);
        }
        if (m_MoveRotation == MoveRotation.Right)
        {
            m_rb.MoveRotation(m_rb.rotation - turnFactor * 12 * Time.fixedDeltaTime);
        }

        if(m_MoveDirection == MoveDirection.None)
        {
            m_rb.velocity = Vector2.Lerp(m_rb.velocity, m_ResetVector, slowFactor * Time.deltaTime);
        }
        if(m_MoveDirection == MoveDirection.Up)
        {
            m_rb.velocity = m_UpVector * speed * 12 * Time.fixedDeltaTime;
        }
        if(m_MoveDirection == MoveDirection.Down)
        {
            m_rb.velocity = -m_UpVector * speed * 10 * Time.fixedDeltaTime;
        }
      
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, weaponLocationPt.position, weaponLocationPt.rotation, weaponParentObject);
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthGears.Length; i++)  //for each possible heart
        {
            if(i < currentHealth)  //if our great index is less than current health fill the heart, do this untill it is not less
            {
                healthGears[i].sprite = fullGear;
            }
            else
            {
                healthGears[i].sprite = emptyGear; //turn the rest of the hearts empty
            }
        }
    }

    public void Heal(int healAmmount)
    {
        if (health + healAmmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmmount;
        }

        UpdateHealthUI(health);
    }


    public void TakeDamage(int damageAmmount)
    {
        health -= damageAmmount;
        UpdateHealthUI(health); //updates the health ui
        HurtPanel(); //updates the hurt panel
        if (health <= 0)
        {
            Death();
        }
    }

    public void LookAtMouse()
    {
        // Distance from camera to object.  We need this to get the proper calculation.
        float camDis = m_cam.transform.position.y - m_transform.position.y;

        // Get the mouse position in world space. Using camDis for the Z axis.
        Vector3 mouse = m_cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));

        float AngleRad = Mathf.Atan2(mouse.y - m_transform.position.y, mouse.x - m_transform.position.x);
        float angle = (180 / Mathf.PI) * AngleRad;

        m_rb.rotation = angle -90;


        Debug.DrawLine(m_transform.position, mouse);
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }


    //Hurt panel stuff
    private void HurtPanel()
    {
        hurtPanel.SetTrigger("hurt");
    }
}

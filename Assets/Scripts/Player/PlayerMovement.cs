using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public CharacterController controller;

    float x, z;
    Vector3 move;

    public float speed;
    float auxSpeed;
    float runSpeed;
    public float gravity = -9.81f;

    Vector3 velocity;

    Vector3 zeroFloat;

    //public AudioClip walkSFX, runSFX;
    public AudioSource aSWalk, aSRun;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Start()
    {
        runSpeed = speed * 2.5f;
        auxSpeed = speed;
        zeroFloat = new Vector3(0.0f, 0.0f, 0.0f);
        //audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //Debug.Log("Move: " + move);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Movement(runSpeed);
            
            if(move != zeroFloat)
            {
                //Debug.Log("Running");
                aSWalk.enabled = false;
                aSRun.enabled = true;
            }
            else
            {
                aSWalk.enabled = false;
                aSRun.enabled = false;
            }

            //audioSource.clip = runSFX;
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            Movement(speed);

            if (move != zeroFloat)
            {
                //Debug.Log("Walking");
                aSWalk.enabled = true;
                aSRun.enabled = false;
            }
            else
            {
                aSWalk.enabled = false;
                aSRun.enabled = false;
            }

            //audioSource.clip = walkSFX;
        }
        //else if(move == float())
        //{
        //    Debug.Log("Quieto");
        //    aSWalk.enabled = false;
        //    aSRun.enabled = false;
        //}
    }

    void Movement(float speed)
    {

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime * 2);
    }

    public void ChangeSpeed()
    {
        if (PlayerManager.instance.isTalking)
        {
            speed = 0;
        }
        else
        {
            speed = auxSpeed;
        }
    }
}

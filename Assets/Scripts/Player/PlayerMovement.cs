using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    float runSpeed;
    public float gravity = -9.81f;

    Vector3 velocity;

    private void Start()
    {
        runSpeed = speed * 2.5f;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Movement(runSpeed);

        }
        else
        {
            Movement(speed);
        }   

        
    }

    void Movement(float speed)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime * 2);
    }
}

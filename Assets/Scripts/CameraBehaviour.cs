using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour instance;

    public float mouseSensitivity;

    float mouseX;
    float mouseY;

    public Transform player;

    float xRotation = 0f;

    public bool canMove;

    public float lockCamera;

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
        //Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        lockCamera = 1;
    }

    private void Update()
    {
        //if (canMove)
        //{
        //    mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * lockCamera;
        //    mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * lockCamera;

        //    xRotation -= mouseY;
        //    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //    player.Rotate(Vector3.up * mouseX);
        //}
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * lockCamera;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * lockCamera;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseX);
    }

    public void LockOnConversation()
    {
        if (PlayerManager.instance.isTalking)
        {
            //canMove = false;
            //mouseX = 0;
            //mouseY = 0;
            lockCamera = 0;
        }
        else
        {
            //canMove = true;
            lockCamera = 1;

        }
    }


}

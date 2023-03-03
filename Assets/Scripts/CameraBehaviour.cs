using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 camPos;
    public Vector3 camLookAt;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.transform.position + camPos;

        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(playerPos.transform.position + camLookAt);

        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        transform.LookAt(playerPos.transform.position + camLookAt, Vector3.up);
    }
}

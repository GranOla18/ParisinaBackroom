using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorBehaviour : MonoBehaviour
{
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex  == 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if(scene.buildIndex == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

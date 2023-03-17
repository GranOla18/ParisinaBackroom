using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBehaviour : MonoBehaviour
{
    Light flashlight;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GetComponent<Light>();
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isOn)
            {
                flashlight.enabled = true;
                isOn = true;
            }
            else
            {
                flashlight.enabled = false;
                isOn = false;
            }
        }
    }
}

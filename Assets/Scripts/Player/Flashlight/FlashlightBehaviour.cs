using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBehaviour : MonoBehaviour
{
    Light flLight;
    bool isOn;
    public float chargeFlash;
    bool canFlash;
    float flashTime;

    // Start is called before the first frame update
    void Start()
    {
        flLight = GetComponent<Light>();
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isOn)
            {
                flLight.enabled = true;
                isOn = true;
            }
            else
            {
                flLight.enabled = false;
                isOn = false;
            }
        }

        if (Input.GetKey(KeyCode.G))
        {
            chargeFlash += Time.deltaTime;

            if(chargeFlash >= 1)
            {
                canFlash = true;
                Debug.Log("cola lampara");
            }
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            chargeFlash = 0;
            flashTime = 0;

            if (canFlash)
            {
                StartCoroutine(FlashRoutine());

            }

            canFlash = false;
            //StartCoroutine(FlashRoutine());
            Debug.Log("se levanto");
        }
    }

    IEnumerator FlashRoutine()
    {
        flLight.intensity = 8;
        flLight.spotAngle = 90;
        yield return new WaitForSeconds(0.35f);
        flLight.intensity = 2;
        flLight.spotAngle = 60;
    }
}

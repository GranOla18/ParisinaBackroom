using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBehaviour : MonoBehaviour
{
    Light flLight;
    bool isOn;
    public float chargeFlash;
    public float flBattery;
    bool canFlash;
    bool isCharging;

    public AudioClip clickSFX;

    // Start is called before the first frame update
    void Start()
    {
        flLight = GetComponent<Light>();
        isOn = true;
        flBattery = 100;

        SFXManager.instance.onReproduceSFX += ClickSFX;
    }

    private void OnDisable()
    {
        SFXManager.instance.onReproduceSFX -= ClickSFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ClickSFX();

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

            if(chargeFlash >= 1 && flBattery >= 40)
            {
                canFlash = true;
            }
            else
            {
                canFlash = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            chargeFlash = 0;

            if (canFlash)
            {
                StartCoroutine(FlashRoutine());
                if (!isCharging)
                {
                    StartCoroutine(ChargeBattery());
                }
            }

            canFlash = false;
        }
    }

    public void ClickSFX()
    {
        SFXManager.instance.audioSource.PlayOneShot(clickSFX);
    }

    IEnumerator FlashRoutine()
    {
        flLight.intensity = 8;
        flLight.spotAngle = 90;
        flBattery -= 40;
        yield return new WaitForSeconds(0.35f);
        flLight.intensity = 2;
        flLight.spotAngle = 60;
    }

    IEnumerator ChargeBattery()
    {
        while (flBattery < 100)
        {
            isCharging = true;
            flBattery += 10;
            yield return new WaitForSeconds(5f);
        }
        isCharging = false;
    }
}

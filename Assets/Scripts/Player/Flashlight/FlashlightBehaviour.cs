using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBehaviour : MonoBehaviour
{
    Light flLight;
    bool isOn;
    public float chargeFlash;
    public float flBattery;
    public float flBattPercent;
    bool canFlash;
    bool isCharging;

    public bool canChargeNext;

    public AudioClip clickSFX;

    public static FlashlightBehaviour instance;

    public delegate void BatteryChange(float newBattery);
    public BatteryChange onBatteryChange;

    public GameObject flashlight;

    Ray flRay;
    RaycastHit flHit;

    public float coladist;

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

    // Start is called before the first frame update
    void Start()
    {
        flLight = GetComponent<Light>();
        isOn = true;
        flBattery = 100;

        SFXManager.instance.onReproduceSFX += ClickSFX;
    }

    private void OnEnable()
    {
        HUD.instance.LinkFlashlight();
    }

    private void OnDisable()
    {
        SFXManager.instance.onReproduceSFX -= ClickSFX;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        ClickSFX();

    //        if (!isOn)
    //        {
    //            flLight.enabled = true;
    //            isOn = true;
    //        }
    //        else
    //        {
    //            flLight.enabled = false;
    //            isOn = false;
    //        }
    //    }

    //    if (Input.GetKey(KeyCode.G))
    //    {
    //        chargeFlash += Time.deltaTime;

    //        if(chargeFlash >= 1 && flBattery >= 40)
    //        {
    //            canFlash = true;
    //        }
    //        else
    //        {
    //            canFlash = false;
    //        }
    //    }

    //    if (Input.GetKeyUp(KeyCode.G))
    //    {
    //        chargeFlash = 0;

    //        if (canFlash)
    //        {
    //            StartCoroutine(FlashRoutine());

    //            if (!isCharging)
    //            {
    //                StartCoroutine(ChargeBattery());
    //            }
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        flRay.origin = transform.position;
        flRay.direction = transform.forward;

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

            if (chargeFlash >= 1 && flBattery >= 40)
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

                if (Physics.Raycast(flRay, out flHit))
                {
                    Debug.DrawLine(flRay.origin, flHit.point, Color.red);
                    //Debug.Log("Ray hit distance " + flHit.distance);
                    //coladist = flHit.distance;

                    if (flHit.collider.GetComponentInChildren<FakeWorkerManager>() && (flHit.distance < 1.5))
                    {
                        //choque.collider.GetComponent<FakeWorkerManager>().ShowFakeMAT();

                        //Debug.Log("pene");
                        flHit.collider.GetComponentInChildren<FakeWorkerManager>().ShowFakeMAT();
                    }
                    else
                    {
                        //Debug.Log("pitito");
                    }
                }

                if (!isCharging)
                {
                    StartCoroutine(ChargeBattery());
                }
            }
        }

        //if (Physics.Raycast(flRay, out flHit))
        //{
        //    Debug.Log("Ray hit distance " + flHit.distance);
        //    coladist = flHit.distance;

        //    Debug.DrawLine(flRay.origin, flHit.point, Color.blue);
        //}
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
        flBattPercent = ((int)flBattery);
        canChargeNext = false;
        if (onBatteryChange != null)
        {
            onBatteryChange.Invoke(flBattPercent);
        }
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
            flBattPercent = (int)flBattery;
            canChargeNext = true;
            StartCoroutine(ChargePercentage(flBattPercent - 10, flBattPercent));
            //Debug.Log("Cola");
            yield return new WaitForSeconds(5f);
            canChargeNext = false;
        }
        isCharging = false;
    }

    IEnumerator ChargePercentage(float valFirst, float valEnd)
    {
        float currentTime = 0;
        while(canChargeNext)
        {
            currentTime += Time.deltaTime;
            flBattPercent = Mathf.Lerp(valFirst, valEnd, currentTime / 5);
            if (onBatteryChange != null)
            {
                onBatteryChange.Invoke(flBattPercent);
            }
            yield return new WaitForEndOfFrame();
        }
        
    }
}

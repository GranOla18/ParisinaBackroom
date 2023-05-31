using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    public Light generalLight;
    private int timesFlick;
    public float flickSpeed;
    public float originalIntensity;

    public Outline guardOutline;
    // Start is called before the first frame update
    void Start()
    {
        //GuardOutline.enabled = false;
        guardOutline.OutlineWidth = 0;
    }

    IEnumerator FlickerRoutine()
    {
        float newIntensity;
        //ghost.Wait();
        //hasFaded = false;
        while (timesFlick < 2)
        {

            newIntensity = Mathf.Lerp(generalLight.intensity, 0.1f, flickSpeed * Time.deltaTime);

            //Debug.Log(newIntensity);

            generalLight.intensity = newIntensity;

            if(newIntensity <= 0.19)
            {
                if(timesFlick < 1)
                {
                    generalLight.intensity = originalIntensity;
                }
                timesFlick++;
            }

            yield return new WaitForEndOfFrame();
        }

        //guardOutline.enabled = true;
        guardOutline.OutlineWidth = 6;

        while (generalLight.intensity < 0.9)
        {
            newIntensity = Mathf.Lerp(generalLight.intensity, originalIntensity, 5* Time.deltaTime);

            generalLight.intensity = newIntensity;
            
            yield return new WaitForEndOfFrame();
        }

        generalLight.intensity = originalIntensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            StartCoroutine(FlickerRoutine());
        }
    }
}

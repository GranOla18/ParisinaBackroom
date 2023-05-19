using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    public Slider slider;
    public float sliderVal;
    public Image panelBrightness;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Brightness", 0f);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, 1 - slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeSlider(float val)
    {
        sliderVal = val;
        PlayerPrefs.SetFloat("Brightness", sliderVal);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, 1 - slider.value);
    }
}

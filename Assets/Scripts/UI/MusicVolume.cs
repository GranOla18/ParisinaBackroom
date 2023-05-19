using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicVolume : MonoBehaviour
{
    public Slider slider;
    public float sliderVal;
    public AudioMixer masterMixer;
    public string param;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVol", 0f);
    }

    public void ChangeVol(float val)
    {
        sliderVal = val;
        slider.value = PlayerPrefs.GetFloat("MusicVol", sliderVal);
        masterMixer.SetFloat(param, slider.value);
    }
}

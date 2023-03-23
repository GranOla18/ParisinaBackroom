using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

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

    public delegate void ReproduceSound();
    public ReproduceSound onReproduceSFX;

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReproduceSFX(int indiceClip)
    {
        //audioSource.Play();
        //audioSource.PlayOneShot(clipSonido[indiceClip]);
        if (onReproduceSFX != null)
        {
            onReproduceSFX.Invoke();
        }
    }
}

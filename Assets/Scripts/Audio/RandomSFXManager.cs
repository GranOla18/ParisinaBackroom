using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFXManager : MonoBehaviour
{
    public AudioClip[] randomSFX;
    AudioSource audioSource;
    public float time;
    public int rndSFX;
    public float randomTime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.onStartGame += StartRadomSFX;
    }

    private void OnDisable()
    {
        GameManager.instance.onStartGame -= StartRadomSFX;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void StartRadomSFX()
    {
        StartCoroutine(PlayRandomSFX());
    }

    IEnumerator PlayRandomSFX()
    {
        while (true)
        {
            randomTime = Random.Range(25.0f, 45.0f);
            yield return new WaitForSeconds(randomTime);
            rndSFX = Random.Range(0, 2);
            audioSource.PlayOneShot(randomSFX[rndSFX]);
            Debug.Log("Playing " + rndSFX);
            time = 0;
        }
    }
}

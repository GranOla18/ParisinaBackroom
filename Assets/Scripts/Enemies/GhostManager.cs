using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : Interactable
{
    public float fadeSpeed, fadeAmount;
    //float originalOpacity = 255;
    Material[] mat;
    public bool bye;
    public GhostAI ghost;
    public float fadeTime;
    public bool hasFaded;
    public Color currentColor;
    Color originalColor;

    bool isFading;

    public AudioClip[] hitSFX;
    AudioSource audioSource;
    public int rndSFX;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().materials;
        audioSource = GetComponent<AudioSource>();
        originalColor = mat[0].color;
    }

    public IEnumerator FadeRoutine()
    {
        float a;
        ghost.Wait();
        hasFaded = false;
        while (!hasFaded)
        {
            currentColor = mat[0].color;
            a = Mathf.Lerp(currentColor.a, 0, 3 * Time.deltaTime);

            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, a);
            //currentColor.a -= 0.2f;
            mat[0].color = smoothColor;
            mat[1].color = smoothColor;
            if (a <= 0.01)
            {
                hasFaded = true;
            }
            yield return new WaitForEndOfFrame();
        }

        ghost.Spawn();
        isFading = false;
    }

    public void SetOriginalColor()
    {
        mat[0].color = originalColor;
        mat[1].color = originalColor;
    }

    public override void EnterTrigger()
    {
        base.EnterTrigger();
        //PlayerManager.instance.GetComponent<IDamage>().Damage();
        if (!PlayerManager.instance.isHidden && !isFading)
        {
            StopAllCoroutines();
            StartCoroutine(FadeRoutine());
            isFading = true;
            PlayerManager.instance.Damage();
            audioSource.Play();
            ghost.agent.isStopped = true;
            Debug.Log("bye");
        }
        else
        {
            StopAllCoroutines();
            ghost.StartCoroutine(ghost.RoutinePatroll());
            Debug.Log("No damage, player is hidden");
        }
    }
}

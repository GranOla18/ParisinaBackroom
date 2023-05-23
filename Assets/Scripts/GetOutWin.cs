using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetOutWin : MonoBehaviour
{
    public static GetOutWin instance;

    public Image blackImg;
    public Image winImage;
    public Canvas winCanvas;
    public GraphicRaycaster graphicRaycaster;

    public Color currentColor;

    public bool hasFadeToBlack;
    public bool hasShownImage;

    public float fadeSpeed;

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

    public IEnumerator FadeToBlackRoutine()
    {
        float a;
        //ghost.Wait();
        //hasFaded = false;
        while (!hasFadeToBlack)
        {

            currentColor = blackImg.color;
            a = Mathf.Lerp(currentColor.a, 1, fadeSpeed * Time.deltaTime);

            blackImg.color = new Color(currentColor.r, currentColor.g, currentColor.b, a);

            if (a >= 0.99)
            {
                hasFadeToBlack = true;
                blackImg.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
            }
            yield return new WaitForEndOfFrame();
        }

        winCanvas.enabled = true;
        StartCoroutine(ShowWinRoutine());

    }

    public IEnumerator ShowWinRoutine()
    {
        float a;
        //ghost.Wait();
        //hasFaded = false;
        while (!hasShownImage)
        {

            currentColor = winImage.color;
            a = Mathf.Lerp(currentColor.a, 1, fadeSpeed * Time.deltaTime);

            winImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, a);

            if (a >= 0.99)
            {
                hasShownImage = true;
                winImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
            }
            yield return new WaitForEndOfFrame();
        }


    }

    [ContextMenu("Fade")]
    public void StartFade()
    {
        StartCoroutine(FadeToBlackRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            StartCoroutine(FadeToBlackRoutine());
            graphicRaycaster.enabled = true;
            GameManager.instance.Win();
        }
    }
}

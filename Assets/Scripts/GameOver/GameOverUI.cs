using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    public Image blackImg;
    public Image gameOverImage;
    public Canvas gameOverCanvas;
    //public GameObject gameOverGO;
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
        gameOverCanvas.enabled = true;

        float a;

        //ghost.Wait();
        //hasFaded = false;
        while (!hasFadeToBlack)
        {
            currentColor = blackImg.color;
            a = Mathf.Lerp(currentColor.a, 1, fadeSpeed * Time.deltaTime);

            blackImg.color = new Color(currentColor.r, currentColor.g, currentColor.b, a);

            if (a >= 0.98)
            {
                hasFadeToBlack = true;
                blackImg.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);

            }
            yield return new WaitForEndOfFrame();
        }

        //gameOverGO.SetActive(true);
        StartCoroutine(ShowGameOverRoutine());

    }

    public IEnumerator ShowGameOverRoutine()
    {
        float a;
        //float aB;

        //Color cc;


        //ghost.Wait();
        //hasFaded = false;
        while (!hasShownImage)
        {
            //cc = blackImg.color;

            currentColor = gameOverImage.color;
            a = Mathf.Lerp(currentColor.a, 1, fadeSpeed * Time.deltaTime);
            //aB = Mathf.Lerp(cc.a, 0, fadeSpeed * Time.deltaTime);

            gameOverImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, a);
            //blackImg.color = new Color(cc.r, cc.g, cc.b, aB);

            if (a >= 0.9)
            {
                hasShownImage = true;
                gameOverImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
                //blackImg.color = new Color(cc.r, cc.g, cc.b, 0);
            }
            yield return new WaitForEndOfFrame();
        }

        graphicRaycaster.enabled = true;

    }

    [ContextMenu("Fade")]
    public void StartFade()
    {
        StartCoroutine(FadeToBlackRoutine());
    }
}

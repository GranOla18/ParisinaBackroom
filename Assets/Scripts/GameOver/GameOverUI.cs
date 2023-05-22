using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameOverUI instance;

    public Image blackImg;
    public Image gameOverImage;
    public Canvas gameOverCanvas;

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

        gameOverCanvas.enabled = true;
        StartCoroutine(ShowGameOverRoutine());

    }

    public IEnumerator ShowGameOverRoutine()
    {
        float a;
        //ghost.Wait();
        //hasFaded = false;
        while (!hasShownImage)
        {

            currentColor = gameOverImage.color;
            a = Mathf.Lerp(currentColor.a, 1, fadeSpeed * Time.deltaTime);

            gameOverImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, a);

            if (a >= 0.99)
            {
                hasShownImage = true;
                gameOverImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
            }
            yield return new WaitForEndOfFrame();
        }


    }

    [ContextMenu("Fade")]
    public void StartFade()
    {
        StartCoroutine(FadeToBlackRoutine());
    }
}

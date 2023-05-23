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
    public GameObject winGO;
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
        winCanvas.enabled = true;

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

        //winGO.SetActive(true);

        StartCoroutine(ShowWinRoutine());

    }

    public IEnumerator ShowWinRoutine()
    {
        float a;
        //float aB;

        //Color blackCurrentColor;
        //ghost.Wait();
        //hasFaded = false;
        while (!hasShownImage)
        {
            //blackCurrentColor = blackImg.color;
            currentColor = winImage.color;

            a = Mathf.Lerp(currentColor.a, 1, fadeSpeed * Time.deltaTime);
            //aB = Mathf.Lerp(blackCurrentColor.a, 0, fadeSpeed * Time.deltaTime);

            winImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, a);
            //blackImg.color = new Color(blackCurrentColor.r, blackCurrentColor.g, blackCurrentColor.b, aB);

            if (a >= 0.99)
            {
                hasShownImage = true;
                winImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
                //blackImg.color = new Color(blackCurrentColor.r, blackCurrentColor.g, blackCurrentColor.b, 0);

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
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(FadeToBlackRoutine());
            graphicRaycaster.enabled = true;
            GameManager.instance.Win();
        }
    }
}

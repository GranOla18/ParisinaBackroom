using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenuCanvas;

    public Canvas warningCanvas;

    public Canvas settingsCanvas;

    public static PauseMenu instance;

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

    public void ShowPauseMenu()
    {
        pauseMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        warningCanvas.enabled = false;
    }

    public void HidePauseMenu()
    {
        pauseMenuCanvas.enabled = false;
        settingsCanvas.enabled = false;
    }

    public void ShowSettings()
    {
        settingsCanvas.enabled = true;
        pauseMenuCanvas.enabled = false;
    }

    public void ShowWarning()
    {
        warningCanvas.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;

    public Button continueBtn;
    public Button settingsBtn;

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
        pauseMenu.enabled = true;
    }

    public void HidePauseMenu()
    {
        pauseMenu.enabled = false;
    }
}

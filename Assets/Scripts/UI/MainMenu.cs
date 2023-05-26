using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas ajustes;
    public Canvas creditos;

    public AudioMixer masterMixer;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenuCanavs()
    {
        mainMenu.enabled = true;
        ajustes.enabled = false;
        creditos.enabled = false;
    }

    public void AjustesCanvas()
    {
        mainMenu.enabled = false;
        ajustes.enabled = true;
        creditos.enabled = false;
    }
    public void CreditosCanvas()
    {
        mainMenu.enabled = false;
        ajustes.enabled = false;
        creditos.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        masterMixer.SetFloat("SFXVol", 0);
    }
}

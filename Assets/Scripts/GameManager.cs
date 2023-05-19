using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused;

    public static GameManager instance;

    public GameManager playerFL;
    public Light workingLight;

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

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.instance.ShowPauseMenu();
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.instance.HidePauseMenu();
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [ContextMenu("Start")]
    public void StartGame()
    {
        RenderSettings.fog = true;
        workingLight.enabled = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        //Debug.LogError("moricion");
    }
}

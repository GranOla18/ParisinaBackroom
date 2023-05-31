using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public bool isPaused;
    bool isShowingFolleto;

    public AudioMixer masterMixer;

    public static GameManager instance;

    public GameManager playerFL;
    //public Light workingLight;
    //public Queue<Light> workingLights;
    public Light[] workingLights;

    public bool playerHasFL;

    public ObjectiveCloth objCloth;

    public GameOverUI gameOverUI;

    public delegate void StartDel();
    public StartDel onStartGame;

    public delegate void GameOverDel();
    public GameOverDel onGameOver;

    public delegate void WinDel();
    public WinDel onWin;

    public bool hasWon;


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
        masterMixer.SetFloat("SFXVol", 0);
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
            if (!isShowingFolleto)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    HUD.instance.ShowMapFolleto(true);
                    isShowingFolleto = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    HUD.instance.ShowMapFolleto(false);
                    isShowingFolleto = false;
                }
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
        masterMixer.SetFloat("SFXVol", -80);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.instance.HidePauseMenu();
        masterMixer.SetFloat("SFXVol", 0);
        isPaused = false;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [ContextMenu("Start")]
    public void StartGame()
    {
        if(onStartGame != null)
        {
            onStartGame.Invoke();
        }
        RenderSettings.fog = true;

        foreach (Light light in workingLights)
        {
            light.enabled = false;
        }

        EnableNPCs.instance.StartGameNPCs();
    }

    [ContextMenu("Game Over")]
    public void GameOver()
    {
        if(onGameOver != null)
        {
            onGameOver.Invoke();
        }
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0;
        PlayerMovement.instance.enabled = false;
        gameOverUI.StartCoroutine(gameOverUI.FadeToBlackRoutine());
        masterMixer.SetFloat("SFXVol", -80);
        Debug.Log("GAME OVER");
    }

    [ContextMenu("Win")]
    public void Win()
    {
        if (onWin != null)
        {
            onWin.Invoke();
        }
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0;
        PlayerMovement.instance.enabled = false;
        masterMixer.SetFloat("SFXVol", -80);
        Debug.Log("WIN");
    }

    public void PlayerGivenFL()
    {
        playerHasFL = true;
        objCloth.enabled = true;
    }
}

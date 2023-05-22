using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused;

    public static GameManager instance;

    public GameManager playerFL;
    public Light workingLight;

    public bool playerHasFL;

    public ObjectiveCloth objCloth;

    public GameOverUI gameOverUI;

    public delegate void GameOverDel();
    public GameOverDel onGameOver;

    public GameObject ghost01;
    public GameObject ghost02;
    //public GhostAI ghost01;
    //public GhostAI ghost02;

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
        RenderSettings.fog = true;
        workingLight.enabled = false;

        ghost01.SetActive(true);
        ghost02.SetActive(true);
    }

    public void GameOver()
    {
        if(onGameOver != null)
        {
            onGameOver.Invoke();
        }
        //Time.timeScale = 0;
        PlayerMovement.instance.enabled = false;
        gameOverUI.StartCoroutine(gameOverUI.FadeToBlackRoutine());
        Debug.Log("GAME OVER");
    }

    public void PlayerGivenFL()
    {
        playerHasFL = true;
        objCloth.enabled = true;
    }
}

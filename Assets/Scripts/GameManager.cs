using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused;
    bool isShowingFolleto;

    public static GameManager instance;

    public GameManager playerFL;
    public Light workingLight;

    public bool playerHasFL;

    public ObjectiveCloth objCloth;

    public GameOverUI gameOverUI;

    public delegate void GameOverDel();
    public GameOverDel onGameOver;

    public delegate void WinDel();
    public WinDel onWin;

    public GameObject ghost01;
    public GameObject ghost02;

    public GameObject taylor;
    public GameObject ticketWorker;

    public GameObject fake01;
    public GameObject fake02;

    public GameObject fakeStand01;
    public GameObject fakeStand02;

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

        ticketWorker.SetActive(true);
        fakeStand01.SetActive(true);
        fakeStand02.SetActive(true);
    }

    public void AppearTaylors()
    {
        taylor.SetActive(true);
        fake01.SetActive(true);
        fake02.SetActive(true);
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
        Debug.Log("WIN");
    }

    public void PlayerGivenFL()
    {
        playerHasFL = true;
        objCloth.enabled = true;
    }
}

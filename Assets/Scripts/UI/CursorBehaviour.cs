using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorBehaviour : MonoBehaviour
{
    Scene scene;

    public static CursorBehaviour instance;

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
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex  == 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if(scene.buildIndex == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.instance.onGameOver += ShowCursor;
            GameManager.instance.onWin += ShowCursor;
        }
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("cola");
    }
}

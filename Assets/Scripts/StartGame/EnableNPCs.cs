using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNPCs : MonoBehaviour
{
    public static EnableNPCs instance;

    public GameObject[] disableOnGame;
    public GameObject[] realWorkers;
    public GameObject[] fakeWorkers;
    public GameObject[] ghosts;
    public GameObject[] taylors;    // Real and Fake

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
        GameManager.instance.onWin += WinGhosts;
    }

    public void StartGameNPCs()
    {
        foreach(GameObject byeWorker in disableOnGame)
        {
            byeWorker.SetActive(false);
        }

        foreach(GameObject workers in realWorkers)
        {
            workers.SetActive(true);
        }

        foreach(GameObject fake in fakeWorkers)
        {
            fake.SetActive(true);
        }

        foreach(GameObject ghost in ghosts)
        {
            ghost.SetActive(true);
        }
    }

    public void AppearTaylors()
    {
        foreach (GameObject taylor in taylors)
            taylor.SetActive(true);
    }

    public void WinGhosts()
    {
        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }
    }
}

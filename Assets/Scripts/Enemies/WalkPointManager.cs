using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPointManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GhostAI>())
        {
            Debug.Log("start coroutine");

            GhostAI ghost = other.GetComponent<GhostAI>();
            ghost.StartCoroutine(ghost.RoutinePatroll());
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<GhostAI>())
    //    {
    //        Debug.Log("start coroutine");

    //        GhostAI ghost = other.GetComponent<GhostAI>();
    //        ghost.StopAllCoroutines();
    //    }
    //}
}

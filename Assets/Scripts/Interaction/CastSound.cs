using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSound : MonoBehaviour
{
    public Transform parentObj;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GhostAI>())
        {
            GhostAI ghost = other.GetComponent<GhostAI>();
            Debug.Log("Casted sound to ghost");
            ghost.FollowSound(parentObj);
            //ghost.StartCoroutine(ghost.RoutinePatroll());
        }
    }
}

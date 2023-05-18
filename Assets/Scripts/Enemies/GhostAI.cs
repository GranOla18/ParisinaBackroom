using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] walkPoints;
    public int walkToIdx;
    public Vector3 target;
    public bool sawPlayer;
    public bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isWaiting = false;
        //GotoNextPoint();
        StartCoroutine(RoutinePatroll());
    }

    // Update is called once per frame
    void Update()
    {
        //if (!agent.pathPending && agent.remainingDistance < 0.5f && !sawPlayer && !isWaiting)
        //    GotoNextPoint();
        //if (!agent.pathPending && agent.remainingDistance < 0.5f && !sawPlayer && !isWaiting)
        //    GotoNextPoint();
        //StartCoroutine(ThinkNextPos());
    }

    public void GotoNextPoint()
    {
        //StartCoroutine(ThinkNextPos());
        walkToIdx = Random.Range(0, 4);
        Debug.Log("Walking to " + walkToIdx);
        agent.SetDestination(walkPoints[walkToIdx].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            sawPlayer = true;
            //agent.updatePosition
            agent.SetDestination(other.transform.position);
            //Debug.Log("Siguiendo al jugador");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            sawPlayer = false;
            GotoNextPoint();
            //Debug.Log("Ya no al jugador");
        }
    }

    public void Spawn()
    {
        //StartCoroutine(cola());
        walkToIdx = Random.Range(1, 4);
        agent.transform.position = walkPoints[walkToIdx].position;
        agent.SetDestination(this.transform.position);
        Debug.Log("boo");
    }

    IEnumerator cola()
    {
        yield return new WaitForSeconds(3);
    }


    public IEnumerator RoutinePatroll()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(3);
        do
        {
            walkToIdx = Random.Range(0, 4);

        } while (agent.destination == walkPoints[walkToIdx].position);
        Debug.Log("Walking to " + walkToIdx);
        agent.SetDestination(walkPoints[walkToIdx].position);
    }

    public IEnumerator RoutineFollowSound(Transform sound)
    {
        agent.SetDestination(sound.position);

        Debug.Log("Waiting");
        yield return new WaitForSeconds(3);

        StartCoroutine(RoutinePatroll());
    }


    IEnumerator ThinkNextPos()
    {
        while (!sawPlayer)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Debug.Log("Waiting");
                isWaiting = true;
                yield return new WaitForSeconds(3);
                GotoNextPoint();
            }
        }
        
        isWaiting = false;
        //GotoNextPoint();
    }

    public void FollowSound(Transform sound)
    {
        Debug.LogError("Following sound " + sound.name + " to " + sound.position);
        //agent.SetDestination(sound.transform.position);
        StartCoroutine(RoutineFollowSound(sound));
    }
}

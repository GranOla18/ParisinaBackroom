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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !sawPlayer)
            GotoNextPoint();
    }

    public void GotoNextPoint()
    {
        walkToIdx = Random.Range(1, 4);
        agent.SetDestination(walkPoints[walkToIdx].position);
        Debug.Log("Walking to " + walkToIdx);
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
}
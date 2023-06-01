using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] walkPoints;
    public int walkToIdx;
    public Vector3 target;
    public bool sawPlayer;
    public bool isWaiting;
    public float timeStopped;
    public GhostManager ghostMan;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isWaiting = false;
        //GotoNextPoint();
        StartCoroutine(RoutinePatroll());
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //if (agent.isStopped)
        //{
        //    timeStopped += Time.deltaTime;
        //    Debug.Log("Estoy parado");
        //}
        //else if (!agent.isStopped)
        //{
        //    timeStopped = 0;
        //    Debug.Log("No estoy parado");
        //}
        //else if(timeStopped >= 4.2)
        //{
        //    Debug.Log("Demasiado tiempo parado, cambio");
        //    agent.isStopped = false;
        //    GotoNextPoint();
        //}
    }

    private void OnEnable()
    {
        GameManager.instance.onGameOver += Wait;
        agent = GetComponent<NavMeshAgent>();
        isWaiting = false;
        //GotoNextPoint();
        //StartCoroutine(RoutinePatroll());
        //Spawn();
        walkToIdx = Random.Range(1, 4);
        agent.transform.position = walkPoints[walkToIdx].position;
    }

    private void OnDisable()
    {
        GameManager.instance.onGameOver -= Wait;
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
        if (other.GetComponent<PlayerManager>() && !PlayerManager.instance.isHidden)
        {
            StopAllCoroutines();
            agent.isStopped = false;
            sawPlayer = true;
            //agent.updatePosition
            agent.SetDestination(other.transform.position);
            audioSource.Play();
            Debug.Log("Siguiendo al jugador");
        }
        //else if (other.GetComponent<WalkPointManager>())
        //{
        //    StartCoroutine(RoutinePatroll());
        //}
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
        StopAllCoroutines();
        agent.isStopped = true;
        ghostMan.SetOriginalColor();
        walkToIdx = Random.Range(1, 4);
        agent.transform.position = walkPoints[walkToIdx].position;
        //agent.SetDestination(this.transform.position);
        StartCoroutine(RoutinePatroll());
        Debug.Log("boo");
    }

    public void Wait()
    {
        //agent.SetDestination(this.transform.position);
        agent.isStopped = true;
    }


    public IEnumerator RoutinePatroll()
    {
        yield return new WaitForSeconds(1.5f);
        agent.isStopped = true;

        //Debug.Log("Waiting");
        yield return new WaitForSeconds(3);
        walkToIdx = Random.Range(0, 4);
        //do
        //{
        //    walkToIdx = Random.Range(0, 4);

        //} while (agent.destination == walkPoints[walkToIdx].position);
        //Debug.Log("Walking to " + walkToIdx);
        //Debug.Log("Patroll");
        agent.SetDestination(walkPoints[walkToIdx].position);
        agent.isStopped = false;

    }

    public IEnumerator RoutineFollowSound(Transform sound)
    {
        agent.SetDestination(sound.position);

        //Debug.Log("Waiting");
        yield return new WaitForSeconds(3);

        StartCoroutine(RoutinePatroll());
    }

    public void FollowSound(Transform sound)
    {
        //Debug.LogError("Following sound " + sound.name + " to " + sound.position);
        agent.SetDestination(sound.transform.position);
        StartCoroutine(RoutinePatroll());
    }
}

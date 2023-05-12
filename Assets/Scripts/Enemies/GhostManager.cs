using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostManager : Interactable
{
    NavMeshAgent agent;
    public Transform[] walkPoints;
    public int walkToIdx;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    public void GotoNextPoint()
    {
        walkToIdx = Random.Range(1, 4);
        agent.SetDestination(walkPoints[walkToIdx].position);
        Debug.Log("Walking to " + walkToIdx);
    }

    public override void EnterTrigger()
    {
        base.EnterTrigger();
        //PlayerManager.instance.GetComponent<IDamage>().Damage();
        PlayerManager.instance.Damage();
    }
}

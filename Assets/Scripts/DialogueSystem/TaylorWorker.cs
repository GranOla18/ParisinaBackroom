using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaylorWorker : MonoBehaviour
{
    public DialogueTrigger dT;

    public float smoothTurn;
    public float workTime;

    private Vector3 targetAngles;
    private Vector3 auxAngle;

    public DialogueTrigger dTEntrega;

    public bool hasTurnAround;

    public Outline outline;

    public bool isWorking;

    IEnumerator WorkRoutine()
    {
        isWorking = true;
        yield return new WaitForSeconds(workTime);
        dTEntrega.enabled = true;
        Debug.Log("Finished working");
        hasTurnAround = false;
        targetAngles = 160 * Vector3.up; // what the new angles should be

        this.GetComponent<Animator>().SetTrigger("Idle");

        while (!hasTurnAround)
        {
            auxAngle = Vector3.Lerp(transform.eulerAngles, targetAngles, smoothTurn * Time.deltaTime); // lerp to new angles
            transform.eulerAngles = auxAngle;
            //Debug.Log(transform.eulerAngles);
            if (transform.eulerAngles.y <= 175)
            {
                transform.eulerAngles = targetAngles + 20 * Vector3.up;
                hasTurnAround = true;
            }
            yield return new WaitForEndOfFrame();
        }

        PlayerManager.instance.hasCloth = true;
        outline.enabled = true;
        isWorking = false;
    }

    private void OnEnable()
    {
        dT = this.GetComponent<DialogueTrigger>();
        dT.onPoseChange += GiveCloth;
        isWorking = false;
    }

    private void OnDisable()
    {
        dT.onPoseChange -= GiveCloth;
        DialogueSystem.instance.onFinishDialogue -= PrepareCloth;
        StopAllCoroutines();
    }

    public void GiveCloth()
    {
        if (DialogueSystem.instance.sentences.Count == 3)
        {
            //DialogueSystem.instance.EndDialogue();
            //hasGivenTicket = true;
            //PlayerManager.instance.hasCloth = true;
            //Debug.Log("Preparing Clotkh");
        }
    }

    public void PrepareCloth()
    {
        if (!PlayerManager.instance.hasCloth)
        {
            Debug.Log("Preparing Cloth");
            targetAngles = 290 * Vector3.up; // what the new angles should be
            StartCoroutine(TurnRoutine());
        }
    }

    IEnumerator TurnRoutine()
    {
        Debug.Log("voy a voltear");

        while (!hasTurnAround)
        {
            auxAngle = Vector3.Lerp(transform.eulerAngles, targetAngles, smoothTurn * Time.deltaTime); // lerp to new angles
            transform.eulerAngles = auxAngle;
            //Debug.Log(transform.eulerAngles);

            if (transform.eulerAngles.y >= 265)
            {
                transform.eulerAngles = targetAngles - 20 * Vector3.up;
                hasTurnAround = true;
            }
            yield return new WaitForEndOfFrame();
        }
        this.GetComponent<Animator>().SetTrigger("Work");

        StartCoroutine(WorkRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            DialogueSystem.instance.onFinishDialogue += PrepareCloth;
        }
    }
}

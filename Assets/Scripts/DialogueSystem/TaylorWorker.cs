using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaylorWorker : MonoBehaviour
{
    public DialogueTrigger dT;

    public float smooth = 1f;

    private Vector3 targetAngles;
    private Vector3 auxAngle;

    public DialogueTrigger dTEntrega;

    public bool hasTurnAround;

    public Outline outline;


    // Start is called before the first frame update
    void Start()
    {

        //targetAngles = transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
        //this.GetComponent<Animator>().SetTrigger("Work");
        //StartCoroutine(WorkRoutine());
    }

    IEnumerator WorkRoutine()
    {
        yield return new WaitForSeconds(3);
        dTEntrega.enabled = true;
        Debug.Log("Finished working");
        hasTurnAround = false;
        targetAngles = 160 * Vector3.up; // what the new angles should be

        this.GetComponent<Animator>().SetTrigger("Idle");

        while (!hasTurnAround)
        {
            auxAngle = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
            transform.eulerAngles = auxAngle;
            Debug.Log(transform.eulerAngles);
            if (transform.eulerAngles.y <= 175)
            {
                transform.eulerAngles = targetAngles + 20 * Vector3.up;
                hasTurnAround = true;
            }
            yield return new WaitForEndOfFrame();
        }

        PlayerManager.instance.hasCloth = true;
        outline.enabled = true;
    }

    private void OnEnable()
    {
        dT = this.GetComponent<DialogueTrigger>();
        dT.onPoseChange += GiveCloth;
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
            auxAngle = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
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
        DialogueSystem.instance.onFinishDialogue += PrepareCloth;
    }
}

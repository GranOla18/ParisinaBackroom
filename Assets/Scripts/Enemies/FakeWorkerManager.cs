using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWorkerManager : MonoBehaviour
{
    public DialogueTrigger dT;

    public float smooth = 1f;

    public Vector3 targetAngles;
    private Vector3 auxAngle;

    public bool hasTurnAround;

    public float stopTurning;

    public float rectifyRot;

    // Start is called before the first frame update
    void Start()
    {
        dT = this.GetComponent<DialogueTrigger>();

        //targetAngles = transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
        //this.GetComponent<Animator>().SetTrigger("Work");
        //StartCoroutine(WorkRoutine());
    }

    private void OnDisable()
    {
        DialogueSystem.instance.onFinishDialogue -= PrepareCloth;
    }


    public void PrepareCloth()
    {
        Debug.Log("Preparing Cloth");
        //targetAngles = 70 * Vector3.up; // what the new angles should be
        //targetAngles = 70 * Vector3.up; // what the new angles should be
        StartCoroutine(TurnRoutine());
    }

    IEnumerator TurnRoutine()
    {
        Debug.Log("voy a voltear");

        while (!hasTurnAround)
        {
            auxAngle = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
            transform.eulerAngles = auxAngle;
            //Debug.Log(transform.eulerAngles);

            //if (transform.eulerAngles.y <= 85)
            if (transform.eulerAngles.y <= stopTurning)
            {
                //transform.eulerAngles = targetAngles + 20 * Vector3.up;
                transform.eulerAngles = targetAngles + rectifyRot * Vector3.up;
                hasTurnAround = true;
            }
            yield return new WaitForEndOfFrame();
        }
        this.GetComponent<Animator>().SetTrigger("Work");

        //StartCoroutine(WorkRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        DialogueSystem.instance.onFinishDialogue += PrepareCloth;
    }
}

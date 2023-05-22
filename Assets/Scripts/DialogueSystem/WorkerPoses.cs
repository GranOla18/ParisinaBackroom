using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPoses : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
        dialogueTrigger.onPoseChange += ChangeWorkerPose;
        //DialogueSystem.instance.onFinishDialogue += ReturnIdle;
    }

    public void ChangeWorkerPose()
    {
        dialogueTrigger.ChangePose();
    }

    //public void ReturnIdle()
    //{
    //    dialogueTrigger.animator.SetTrigger("Idle");
    //}

    private void OnDisable()
    {
        dialogueTrigger.onPoseChange -= ChangeWorkerPose;
        //DialogueSystem.instance.onFinishDialogue -= ReturnIdle;
    }
}

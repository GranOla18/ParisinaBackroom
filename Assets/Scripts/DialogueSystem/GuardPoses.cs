using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPoses : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public Animator animator;

    [ContextMenu("Next Sentence")]
    public void ChangeGuardPose()
    {
        if (DialogueSystem.instance.sentences.Count == 6)
        {
            animator.SetTrigger("Give");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    public void Start()
    {
        dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
        dialogueTrigger.onPoseChange += ChangeGuardPose;
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        dialogueTrigger.onPoseChange -= ChangeGuardPose;
    }
}

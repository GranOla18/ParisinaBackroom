using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPoses : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public Animator animator;

    public GameObject flPlayer;
    public GameObject flGuard;

    [ContextMenu("Next Sentence")]
    public void ChangeGuardPose()
    {
        if (DialogueSystem.instance.sentences.Count == 5)
        {
            animator.SetTrigger("Give");
        }
        else if(DialogueSystem.instance.sentences.Count == 4)
        {
            GiveFL();
            animator.SetTrigger("Idle");
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

    public void GiveFL()
    {
        flPlayer.SetActive(true);
        flGuard.SetActive(false);
    }
}

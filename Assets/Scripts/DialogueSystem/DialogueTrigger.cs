using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Animator animator;

    public Dialogue dialogue;

    //public string npcName;

    public bool isTalking;

    public float distanceObj;

    public Vector3 talkPos;

    private Queue<string> poses;

    [ContextMenu("Trigger Dialogue")]
    public void TriggerDialogue()
    {
        DialogueSystem.instance.StartDialogue(dialogue);
        PlayerManager.instance.isTalking = true;
        PlayerMovement.instance.ChangeSpeed();
        CameraBehaviour.instance.LockOnConversation();

        foreach (Dialogue.Pose pose in dialogue.poses)
        {
            //Debug.Log(pose.ToString());
            poses.Enqueue(pose.ToString());
        }

        animator.SetTrigger(poses.Dequeue());

    }

    public override void Interact()
    {
        base.Interact();
        if (!PlayerManager.instance.isTalking)
        {
            TriggerDialogue();
            Debug.Log("Hablando");
        }
        else
        {
            ShowNextSentence();
        }
    }

    [ContextMenu("Next Sentence")]
    public void ShowNextSentence()
    {
        DialogueSystem.instance.DisplayNextSentence();

        if (poses.Count == 0)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger(poses.Dequeue());
        }
    }

    public void Start()
    {
        dialogue.SetName(dialogue.name);
        poses = new Queue<string>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        if (!PlayerManager.instance.isTalking)
        {
            distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position + talkPos);
            //distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
            CheckDistance(distanceObj);
        }

        else if (PlayerManager.instance.isTalking)
        {
            CheckDistance(distanceObj);
        }
    }
}

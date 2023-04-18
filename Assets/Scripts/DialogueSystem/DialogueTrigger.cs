using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;
    public string npcName;

    public bool isTalking;

    float distanceObj;


    [ContextMenu("Trigger Dialogue")]
    public void TrigegrDialogue()
    {
        DialogueSystem.instance.StartDialogue(dialogue);
        PlayerManager.instance.isTalking = true;
        PlayerMovement.instance.ChangeSpeed();
        CameraBehaviour.instance.LockOnConversation();
    }

    public override void Interact()
    {
        base.Interact();
        TrigegrDialogue();
    }

    [ContextMenu("Next Sentence")]
    public void ShowNextSentence()
    {
        DialogueSystem.instance.DisplayNextSentence();
    }

    public void Start()
    {
        dialogue.SetName(npcName);
    }

    public void Update()
    {
        if (!PlayerManager.instance.isTalking)
        {
            distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
            CheckDistance(distanceObj);
        }
        else
        {
            //cola
        }
    }
}

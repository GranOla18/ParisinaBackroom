using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;
    //public string npcName;

    public bool isTalking;

    public float distanceObj;


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
        if (!PlayerManager.instance.isTalking)
        {
            TrigegrDialogue();
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
    }

    public void Start()
    {
        dialogue.SetName(dialogue.name);
    }

    public void Update()
    {
        //if (!PlayerManager.instance.isTalking)
        //{
        //    distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
        //    CheckDistance(distanceObj);
        //}
        //else
        //{
        //    Debug.Log("cola");
        //    //if (Input.GetKeyDown(KeyCode.E))
        //    //{
        //    //    Debug.Log("Mostrar oracion");
        //    //    ShowNextSentence();
        //    //}

        //}
        distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
        CheckDistance(distanceObj);
    }
}

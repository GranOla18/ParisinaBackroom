using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public string npcName;

    [ContextMenu("Trigger Dialogue")]
    public void TrigegrDialogue()
    {
        DialogueSystem.instance.StartDialogue(dialogue);
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
}

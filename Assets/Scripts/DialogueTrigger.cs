using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    [ContextMenu("Trigger Dialogue")]
    public void TrigegrDialogue()
    {
        DialogueSystem.instance.StartDialogue(dialogue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketWorker : MonoBehaviour
{
    public bool hasGivenTicket;
    string sentence;
    public DialogueTrigger dialogueTriggerSinTicket;
    public DialogueTrigger dialogueTriggerConTicket;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTriggerSinTicket = this.gameObject.GetComponent<DialogueTrigger>();
        dialogueTriggerSinTicket.onPoseChange += TickerWorkerActions;
        //DialogueSystem.instance.onFinishDialogue += ChangeDialogue;
    }

    private void OnDisable()
    {
        dialogueTriggerSinTicket.onPoseChange -= TickerWorkerActions;
        //DialogueSystem.instance.onFinishDialogue -= ChangeDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TickerWorkerActions()
    {
        if (DialogueSystem.instance.sentences.Count == 1)
        {
            //DialogueSystem.instance.EndDialogue();
            //hasGivenTicket = true;
            PlayerManager.instance.hasTicket = true;
        }
        else if (PlayerManager.instance.hasTicket)
        {
            ChangeDialogue();
        }
    }

    public void ChangeDialogue()
    {
        dialogueTriggerSinTicket.enabled = false;
        dialogueTriggerConTicket.enabled = true;
    }

}

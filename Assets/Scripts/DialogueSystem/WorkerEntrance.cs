using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerEntrance : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger01;
    public DialogueTrigger dialogueTrigger02;

    public GameObject folletoGameObject;

    [ContextMenu("Next Sentence")]
    public void CheckSentence()
    {
        if(DialogueSystem.instance.sentences.Count == 3)
        {
            StopOutline();
        }
        else if (DialogueSystem.instance.sentences.Count == 2)
        {
            GiveFolleto();
        }
    }

    public void Start()
    {
        dialogueTrigger01 = this.gameObject.GetComponent<DialogueTrigger>();
        dialogueTrigger01.onPoseChange += CheckSentence;
        //DialogueSystem.instance.onFinishDialogue += StopOutline;
        DialogueSystem.instance.onStartDialogue += StopOutline;
    }

    private void OnDisable()
    {
        dialogueTrigger01.onPoseChange -= CheckSentence;
        //DialogueSystem.instance.onFinishDialogue -= StopOutline;
        DialogueSystem.instance.onStartDialogue -= StopOutline;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PlayerManager.instance.hasFolleto)
        {
            dialogueTrigger01.enabled = true;
        }
        else
        {
            dialogueTrigger01.enabled = false;
            dialogueTrigger02.enabled = true;
        }
    }

    public void GiveFolleto()
    {
        folletoGameObject.SetActive(true);
        PlayerManager.instance.hasFolleto = true;
    }

    public void StopOutline()
    {
        this.GetComponent<Outline>().enabled = false;
    }
}

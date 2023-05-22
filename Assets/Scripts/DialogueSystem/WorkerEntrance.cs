using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerEntrance : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;

    public GameObject folletoGameObject;

    [ContextMenu("Next Sentence")]
    public void CheckSentence()
    {
        if (DialogueSystem.instance.sentences.Count == 2)
        {
            GiveFolleto();
        }
    }

    public void Start()
    {
        dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
        dialogueTrigger.onPoseChange += CheckSentence;
    }

    private void OnDisable()
    {
        dialogueTrigger.onPoseChange -= CheckSentence;
    }

    public void GiveFolleto()
    {
        folletoGameObject.SetActive(true);
    }
}

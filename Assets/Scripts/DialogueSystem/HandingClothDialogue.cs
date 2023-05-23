using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandingClothDialogue : MonoBehaviour
{
    public GameObject getOutTrigger;
    public Outline outline;

    private void OnEnable()
    {
        DialogueSystem.instance.onStartDialogue += HandingCloth;
    }

    private void OnDisable()
    {
        //DialogueSystem.instance.onFinishDialogue -= HandingCloth;
        DialogueSystem.instance.onStartDialogue -= HandingCloth;
    }

    public void HandingCloth()
    {
        GameManager.instance.hasWon = true;
        getOutTrigger.SetActive(true);
        outline.enabled = false;
        //StopAllCoroutines();
    }
}

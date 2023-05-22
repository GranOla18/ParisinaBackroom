using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCloth : Interactable
{
    float distanceObj;
    public Outline outlineScriptHead;
    public Outline outlineScriptBody;

    // Start is called before the first frame update
    void Start()
    {
        //dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
        //dialogueTrigger.onFinishDialogue += StartGameTrigger;
        outlineScriptHead = this.GetComponent<Outline>();
        outlineScriptBody.enabled = false;
        outlineScriptHead.enabled = false;
        this.enabled = false;

    }

    private void OnEnable()
    {
        outlineScriptHead = this.GetComponent<Outline>();
        outlineScriptHead.enabled = true;
        outlineScriptBody.enabled = true;
    }

    private void OnDisable()
    {
        DialogueSystem.instance.onFinishDialogue -= StartGameTrigger;
        //dialogueTrigger.onFinishDialogue -= StartGameTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
        CheckDistance(distanceObj);
    }

    public override void Interact()
    {
        base.Interact();
        DialogueSystem.instance.onFinishDialogue += StartGameTrigger;
        //Hide();
    }

    public void StartGameTrigger()
    {
        Debug.Log("cola");
        GameManager.instance.StartGame();
        outlineScriptHead.enabled = false;
        outlineScriptBody.enabled = false;
        this.enabled = false;
    }
}

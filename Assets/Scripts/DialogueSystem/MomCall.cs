using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomCall : MonoBehaviour
{
    public Image cellIcon;

    public DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
        //dialogueTrigger.onPoseChange += MomDialogue;

        DialogueSystem.instance.onFinishDialogue += MomEndCall;

    }

    private void OnDisable()
    {
        DialogueSystem.instance.onFinishDialogue -= MomEndCall;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            cellIcon.enabled = true;
            dialogueTrigger.Interact();
        }
    }

    //public void MomDialogue()
    //{
    //    if (DialogueSystem.instance.sentences.Count == 3)
    //    {
    //        animator.SetTrigger("Give");
    //    }
    //    else if (DialogueSystem.instance.sentences.Count == 4)
    //    {
    //        GiveFL();
    //        animator.SetTrigger("Idle");
    //    }
    //    else
    //    {
    //        animator.SetTrigger("Idle");
    //    }
    //}

    public void MomEndCall()
    {
        //this.gameObject.SetActive(false);
        Debug.Log("cola");
    }



    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    dialogueTrigger.Interact();
        //}
    }
}

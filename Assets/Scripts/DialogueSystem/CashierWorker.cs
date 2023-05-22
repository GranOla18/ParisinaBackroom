using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierWorker : MonoBehaviour
{
    public DialogueTrigger dT;

    // Start is called before the first frame update
    void Start()
    {
        dT = GetComponent<DialogueTrigger>();
        dT.onPoseChange += CashierPay;
    }

    private void OnDisable()
    {
        dT.onPoseChange -= CashierPay;
    }

    public void CashierPay()
    {
        Debug.Log("cambio");
        if (DialogueSystem.instance.sentences.Count == 1)
        {
            //DialogueSystem.instance.EndDialogue();
            //hasGivenTicket = true;
            PlayerManager.instance.hasPaid = true;
            Debug.Log("Player has paid");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    public DialogueTrigger dialogueTriggerConTicket;
    public DialogueTrigger dialogueTriggerSinTicket;

    // Start is called before the first frame update
    void Start()
    {
        //dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            if (PlayerManager.instance.hasTicket)
            {
                dialogueTriggerConTicket.enabled = true;
                dialogueTriggerSinTicket.enabled = false;
            }
            else
            {
                dialogueTriggerSinTicket.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

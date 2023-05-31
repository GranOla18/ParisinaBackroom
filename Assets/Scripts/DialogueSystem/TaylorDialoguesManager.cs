using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaylorDialoguesManager : MonoBehaviour
{
    public DialogueTrigger dTEntrega;
    public DialogueTrigger dTConTicket;
    public DialogueTrigger dTFinish;
    public TaylorWorker tWorker;
    public HandingClothDialogue handingCloth;

    private void OnTriggerEnter(Collider other)
    {
        // Tiene el ticket, ya pago pero no tiene tela = Se atiende
        if (PlayerManager.instance.hasTicket && PlayerManager.instance.hasPaid && !PlayerManager.instance.hasCloth && !GameManager.instance.hasWon)
        {
            //dTSinTicket.enabled = false;
            dTConTicket.enabled = true;
        }
        // Tiene tela, ya acabo = Decir que salga
        else if (PlayerManager.instance.hasCloth && GameManager.instance.hasWon)
        {
            Debug.Log("Get Out");
            //dTSinTicket.enabled = false;
            handingCloth.enabled = false;
            dTEntrega.enabled = false;
            dTFinish.enabled = true;
        }
        // Tiene tela, no ha ganado = Entregar tela
        else if (PlayerManager.instance.hasCloth && !GameManager.instance.hasWon)
        {
            Debug.Log("Entrega active");
            dTConTicket.enabled = false;
            tWorker.enabled = false;
            handingCloth.enabled = true;
            dTEntrega.enabled = true;
        }
    }

}

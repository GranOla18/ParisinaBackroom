using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickUp : Interactable
{
    float distanceObj;
    bool isPickedUp;

    public GameObject flPlayer;

    public override void Interact()
    {
        base.Interact();
        PickUp();
        this.gameObject.SetActive(false);
    }

    void PickUp()
    {
        Debug.Log("Picked up " + this.name);
        isPickedUp = true;
        flPlayer.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPickedUp)
        {
            distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
            CheckDistance(distanceObj);
        }
    }
}

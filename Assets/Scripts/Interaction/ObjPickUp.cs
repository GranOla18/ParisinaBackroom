using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPickUp : Interactable
{
    float distanceObj;
    bool isPickedUp;
    public float thowForce;
    Rigidbody rb;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picked up");
        isPickedUp = true;
        transform.parent = CameraBehaviour.instance.transform;
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.isKinematic = true;
    }

    void Throw()
    {
        Debug.Log("Throw");
        transform.parent = null;
        rb.useGravity = true;
        rb.freezeRotation = false;
        rb.isKinematic = false;
        rb.velocity = CameraBehaviour.instance.transform.forward * thowForce * Time.deltaTime; ;
        isPickedUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPickedUp)
        {
            distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
            CheckDistance(distanceObj);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Throw();
            }
        }
    }
}

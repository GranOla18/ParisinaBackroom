using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPickUp : Interactable
{
    float distanceObj;
    bool isPickedUp;
    public float thowForce;
    Rigidbody rb;
    public GameObject cast;

    AudioSource audioSource;

    public bool hasBeenThrown;

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
        cast.SetActive(false);
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
        hasBeenThrown = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        hasBeenThrown = false;
        audioSource = GetComponent<AudioSource>();
        cast.SetActive(false);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenThrown)
        {
            audioSource.Play();
            StartCoroutine(CastSFX());
        }
    }

    public IEnumerator CastSFX()
    {
        cast.SetActive(true);
        yield return new WaitForSeconds(2);
        cast.SetActive(false);
    }
}
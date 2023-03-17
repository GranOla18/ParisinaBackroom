using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountCloth : Interactable
{
    public Camera hideCamera;
    public bool isHidden;
    float distanceObj;

    // Start is called before the first frame update
    void Start()
    {
        hideCamera = GetComponentInChildren<Camera>();
        isHidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHidden)
        {
            distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
            CheckDistance(distanceObj);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitCloth();
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        Hide();
    }

    public void Hide()
    {
        Debug.LogError("Escondiendo");
        CameraBehaviour.instance.GetComponent<Camera>().enabled = false;
        hideCamera.enabled = true;
        isHidden = true;
    }

    public void ExitCloth()
    {
        Debug.Log("Saliendo");
        CameraBehaviour.instance.GetComponent<Camera>().enabled = true;
        PlayerManager.instance.GetComponent<Transform>().rotation = hideCamera.transform.rotation;
        hideCamera.enabled = false;
        isHidden = false;
    }
}

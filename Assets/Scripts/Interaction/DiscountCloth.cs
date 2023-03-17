using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountCloth : Interactable
{
    public Camera hideCamera;
    public bool isHidden;
    float distanceObj;

    PlayerManager pM;

    // Start is called before the first frame update
    void Start()
    {
        hideCamera = GetComponentInChildren<Camera>();
        isHidden = false;
        pM = PlayerManager.instance;
        pM.isHidden = false;

        PlayerManager.instance.onChokeBreath += ExitCloth;
    }

    private void OnDisable()
    {
        PlayerManager.instance.onChokeBreath -= ExitCloth;
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
        pM.isHidden = true;
    }

    public void ExitCloth()
    {
        Debug.Log("Saliendo");
        CameraBehaviour.instance.GetComponent<Camera>().enabled = true;
        PlayerManager.instance.GetComponent<Transform>().rotation = hideCamera.transform.rotation;
        hideCamera.enabled = false;
        isHidden = false;
        pM.isHidden = false;
    }
}

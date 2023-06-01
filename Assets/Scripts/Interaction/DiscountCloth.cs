using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountCloth : Interactable
{
    public Camera hideCamera;
    public bool isHidden;
    float distanceObj;

    PlayerManager pM;

    public AudioClip hide01;
    public AudioClip hide02;

    // Start is called before the first frame update
    void Start()
    {
        hideCamera = GetComponentInChildren<Camera>();
        isHidden = false;
        pM = PlayerManager.instance;
        pM.isHidden = false;

        pM.timeHidden = 0;
    }

    private void OnDisable()
    {
        //PlayerManager.instance.onChokeBreath -= ExitCloth;
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
            pM.timeHidden += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitCloth();
            }
        }
    }

    public override void Interact()
    {
        pM.onChokeBreath += ExitCloth;
        base.Interact();
        Hide();
    }

    public void Hide()
    {
        Debug.Log("Escondiendo");

        CameraBehaviour.instance.GetComponent<Camera>().enabled = false;
        hideCamera.enabled = true;
        isHidden = true;
        pM.isHidden = true;
        PlayerMovement.instance.enabled = false;
        PlayerManager.instance.flashlight.SetActive(false);
        StartCoroutine(pM.Choke());
    }

    public void ExitCloth()
    {
        Debug.Log("Saliendo");

        int sfx = Random.Range(0, 1);

        PlayerMovement.instance.enabled = true;
        if (GameManager.instance.playerHasFL)
        {
            PlayerManager.instance.flashlight.SetActive(true);
        }

        if (sfx == 0)
        {
            SFXManager.instance.audioSource.PlayOneShot(hide01);
        }
        else
        {
            SFXManager.instance.audioSource.PlayOneShot(hide02);
        }

        CameraBehaviour.instance.GetComponent<Camera>().enabled = true;
        PlayerManager.instance.GetComponent<Transform>().rotation = hideCamera.transform.rotation;
        hideCamera.enabled = false;
        isHidden = false;
        pM.isHidden = false;
        pM.timeHidden = 0;
        pM.onChokeBreath -= ExitCloth;
        StartCoroutine(pM.RecoverBeath());
        //StartCoroutine(pM.RecoverBeath());
    }
}

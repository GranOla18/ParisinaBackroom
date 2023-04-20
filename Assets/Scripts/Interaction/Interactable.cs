    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float radius;

    [SerializeField]
    //float distance;

    public Image popUpMgs;

    public bool canDamage;

    void Start()
    {
        PopUp(false);
        
    }

    public virtual void CheckDistance(float distance)
    {
        if (distance <= radius)
        {
            //Debug.Log("Can Interact with " + name);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + this.name);
        PopUp(false);
        //PopUp();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void PopUp(bool show)
    {
        if (show)
        {
            popUpMgs.enabled = true;
        }
        else
        {
            popUpMgs.enabled = false;
        }
    }

    public virtual void EnterTrigger()
    {
        Debug.Log("Attack Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            if (!canDamage)
            {
                PopUp(true);
            }
            else
            {
                EnterTrigger();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        PopUp(false);  
    }
}

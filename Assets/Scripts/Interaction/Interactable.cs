    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    // FFD200
    // 8C7300

    public float radius;

    [SerializeField]
    //float distance;

    public Image popUpMgs;

    public Outline outline;

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
                if (outline)
                {
                    outline.OutlineWidth = 0;
                }

                Interact();
            }
        }
        else
        {
            if (outline)
            {
                outline.OutlineWidth = 0;
            }
        }
    }

    public virtual void Interact()
    {
        //Debug.Log("Interacting with " + this.name);
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
                
                if (outline)
                {
                    outline.OutlineWidth = 6;
                }
            }
            else
            {
                EnterTrigger();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!canDamage)
        {
            PopUp(false);
        }

        if (outline)
        {
            outline.OutlineWidth = 0;
        }
    }
}

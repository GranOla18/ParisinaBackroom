    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius;

    [SerializeField]
    float distance;

    private void Update()
    {
        //distance = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);

        //CheckDistance(distance);
    }

    public virtual void CheckDistance(float distance)
    {
        if (distance <= radius)
        {
            Debug.Log("Can Interact with " + name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + this.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

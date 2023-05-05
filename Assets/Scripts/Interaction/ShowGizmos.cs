using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGizmos : MonoBehaviour
{
    public float radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
        //Gizmos.DrawCube(transform.position, )
    }
}

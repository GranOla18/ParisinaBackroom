using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Hurt()
    {
        base.Hurt();
        //PlayerManager.instance.GetComponent<IDamage>().Damage();
        PlayerManager.instance.Damage();
    }
}

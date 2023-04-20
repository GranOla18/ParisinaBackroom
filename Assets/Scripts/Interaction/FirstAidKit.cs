using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : Interactable
{
    float distanceObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.health < 3)
        {
            distanceObj = Vector3.Distance(PlayerManager.instance.transform.position, this.transform.position);
            CheckDistance(distanceObj);
        }
    }

    public override void Interact()
    {
        base.Interact();
        PlayerManager.instance.Heal();
    }

    //public override void EnterTrigger()
    //{
    //    base.EnterTrigger();
    //    //PlayerManager.instance.GetComponent<IDamage>().Damage();
    //    PlayerManager.instance.Heal();
    //}
}

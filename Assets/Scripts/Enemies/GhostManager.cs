using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : Interactable
{
    public float fadeSpeed, fadeAmount;
    float originalOpacity = 255;
    Material[] mat;
    public bool bye;
    public GhostAI ghost;

    private void Start()
    {
        mat = GetComponent<SkinnedMeshRenderer>().materials;
    }

    void Fade()
    {
        Color currentColor = mat[0].color;
    }

    public override void EnterTrigger()
    {
        base.EnterTrigger();
        //PlayerManager.instance.GetComponent<IDamage>().Damage();
        if (!PlayerManager.instance.isHidden)
        {
            PlayerManager.instance.Damage();
            Debug.Log("bye");
            ghost.Spawn();
        }
        else
        {
            Debug.Log("No damage, player is hidden");
        }
    }
}

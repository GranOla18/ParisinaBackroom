using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWorkerReveal : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    public Material fakeMAT;
    public Material realMAT;

    Material[] fakeMATS;
    Material[] realMATS;

    // Start is called before the first frame update
    void Start()
    {
        fakeMATS = new Material[] { realMAT, fakeMAT };
        realMATS = new Material[] { realMAT, realMAT };
    }

    [ContextMenu("Show")]
    public void ShowFakeMAT()
    {
        StartCoroutine(ShowFake());
    }

    IEnumerator ShowFake()
    {
        skin.materials = fakeMATS;
        //Debug.Log("changed");
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("return");
        skin.materials = realMATS;
    }
}

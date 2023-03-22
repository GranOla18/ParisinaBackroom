using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamage
{
    public static PlayerManager instance;

    public int health;

    public bool isHidden;
    public float timeHidden;

    public int maxBreath;
    public int breath;

    public delegate void DamageRecieved(float actualHealth);
    public DamageRecieved onDamageRecieved;

    public delegate void ChokeCloth();
    public ChokeCloth onChokeBreath;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        breath = maxBreath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        health -= 1;

        if (onDamageRecieved != null)
        {
            onDamageRecieved.Invoke(health);
        }
    }

    public IEnumerator Choke()
    {
        while(isHidden && breath > 0)
        {
            yield return new WaitForSeconds(1);
            breath -= 1;
        }

        if (breath <= 0)
        {
            GetOutCloth();
        }
    }

    public IEnumerator RecoverBeath()
    {
        while((!isHidden) && (breath < maxBreath))
        {
            yield return new WaitForSeconds(2);
            breath += 1;
        }

        if(breath == maxBreath)
        {
            //Debug.Log("Done breathing");
        }
    }

    [ContextMenu("GetOutCloth")]
    public void GetOutCloth()
    {
        if(onChokeBreath != null)
        {
            onChokeBreath.Invoke();
            //StartCoroutine(RecoverBeath());
        }
    }
}

public interface IDamage
{
    void Damage();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamage
{
    public static PlayerManager instance;

    public int health;

    public bool isHidden;

    public int breath;

    public int maxBreath;

    public float timeHidden;

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

    IEnumerator Choke()
    {
        while(breath > 0)
        {
            breath -= 1;
            yield return new WaitForSeconds(1);
        }

        if(breath <= 0)
        {
            GetOutCloth();
        }
    }

    [ContextMenu("Cola")]
    public void GetOutCloth()
    {
        if(onChokeBreath != null)
        {
            onChokeBreath.Invoke();
        }
    }
}

public interface IDamage
{
    void Damage();
}
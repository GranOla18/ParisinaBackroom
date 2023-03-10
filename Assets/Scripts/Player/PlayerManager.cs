using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamage
{
    public static PlayerManager instance;

    public int health;

    public delegate void DamageRecieved(float actualHealth);
    public DamageRecieved onDamageRecieved;

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
}

public interface IDamage
{
    void Damage();
}
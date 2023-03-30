using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamage
{
    public static PlayerManager instance;

    public int health;
    public int maxHealth;
    public float healthPercent;
    public bool isHealing;

    public bool isHidden;
    public float timeHidden;

    public int maxBreath;
    public int breath;

    public delegate void ChangeHealth(float actualHealth);
    public ChangeHealth onHealthChanged;

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

    [ContextMenu("Damage")]
    public void Damage()
    {
        if(health == 1)
        {
            //TODO: GAME OVER
            Debug.Log("Moricion");
        }
        health -= 1;

        if (onHealthChanged != null)
        {
            onHealthChanged.Invoke(health);
        }
    }

    [ContextMenu("Heal")]
    public void Heal()
    {
        healthPercent = health;
        //health = 3;
        isHealing = true;
        if(health < 2)
        {
            StartCoroutine(RecoverHealthRoutine(healthPercent, maxHealth, 6));
        }
        else if(health >= 2)
        {
            StartCoroutine(RecoverHealthRoutine(healthPercent, maxHealth, 3));
        }
    }

    IEnumerator RecoverHealthRoutine(float valFirst, float valEnd, int healSpeed)
    {
        float currentTime = 0;
        while (healthPercent < maxHealth)
        {
            currentTime += Time.deltaTime;
            healthPercent = Mathf.Lerp(valFirst, valEnd, currentTime / healSpeed);

            //Debug.Log(healthPercent);
            if (onHealthChanged != null)
            {
                onHealthChanged.Invoke(healthPercent);
            }
            health = (int)healthPercent;
            yield return new WaitForEndOfFrame();
        }
        isHealing = false;
        health = maxHealth;

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
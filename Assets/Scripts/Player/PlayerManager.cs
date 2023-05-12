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

    public bool isTalking;

    public int maxBreath;
    public int breath;
    public float breathPercent;
    public bool isBreathChanging;
    public float breathSpeed;
    public float chokeSpeed;

    public delegate void ChangeHealth(float actualHealth);
    public ChangeHealth onHealthChanged;

    public delegate void ChokeCloth();
    public ChokeCloth onChokeBreath;

    public delegate void ChangeBreath(float actualBreath);
    public ChangeBreath onBreathChanged;

    public AudioClip curacion;

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
        isTalking = false;
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

        SFXManager.instance.audioSource.PlayOneShot(curacion);
    }

    public IEnumerator Choke()
    {
        //isBreathChanging = false;

        while (isHidden && breath > 0)
        {
            //yield return new WaitForSeconds(1);
            //breathPercent = breath;
            breathPercent = (int)breath;
            isBreathChanging = true;
            StartCoroutine(ChangeBreathLerpRoutine(breathPercent, breathPercent - 1, chokeSpeed));
            breath -= 1;

            yield return new WaitForSeconds(1);
            //isBreathChanging = false;

        }
        //isBreathChanging = false;



        if (breath <= 0)
        {
            GetOutCloth();
            isBreathChanging = false;
        }
    }

    public IEnumerator ChangeBreathLerpRoutine(float valFirst, float valEnd, float speed)
    {
        float currentTime = 0;
        while (isBreathChanging)
        {
            currentTime += Time.deltaTime;
            breathPercent = Mathf.Lerp(valFirst, valEnd, currentTime / speed);

            //Debug.Log(healthPercent);
            if (onBreathChanged != null)
            {
                onBreathChanged.Invoke(breathPercent);
                //StartCoroutine(RecoverBeath());
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator RecoverBeath()
    {
        //isBreathChanging = false;

        while ((!isHidden) && (breath < maxBreath))
        {
            breathPercent = breath;
            isBreathChanging = true;
            StartCoroutine(ChangeBreathLerpRoutine(breathPercent, breathPercent + 1, breathSpeed));
            breath += 1;

            yield return new WaitForSeconds(2);
            //isBreathChanging = false;
        }


        if (breath == maxBreath)
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
            breath = 0;
            //StartCoroutine(RecoverBeath());
        }
    }
}

public interface IDamage
{
    void Damage();
}
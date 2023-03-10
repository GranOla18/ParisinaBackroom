using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    //public Image healthBar;
    //public Gradient gradient;

    private void Start()
    {
        PlayerManager.instance.onDamageRecieved += ModifyHealth;
    }

    private void OnDisable()
    {
        PlayerManager.instance.onDamageRecieved -= ModifyHealth;
    }

    public void ModifyHealth(float newHealth)
    {
        //healthBar.fillAmount = newHealth;
        //healthBar.color = gradient.Evaluate(1 - newHealth);
    }
}

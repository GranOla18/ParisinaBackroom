using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image healthBar;
    public Gradient gradient;

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
        healthBar.fillAmount = newHealth / 3;
        healthBar.color = gradient.Evaluate(healthBar.fillAmount);
    }
}

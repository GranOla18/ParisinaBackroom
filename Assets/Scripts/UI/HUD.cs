using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image healthBar;
    public Gradient gradient;

    public Image flashlightBar;

    public Image breathBar;

    public static HUD instance;

    public delegate void UpdateHUD();
    public UpdateHUD onUpdateHUD;

    public Canvas folletoCanvas;

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

    private void Start()
    {
        PlayerManager.instance.onHealthChanged += ModifyHealth;
        PlayerManager.instance.onBreathChanged += ModifyBreath;
    }

    private void OnDisable()
    {
        PlayerManager.instance.onHealthChanged -= ModifyHealth;
        FlashlightBehaviour.instance.onBatteryChange -= ModifyBattery;
        PlayerManager.instance.onBreathChanged -= ModifyBreath;
    }

    public void LinkFlashlight()
    {
        FlashlightBehaviour.instance.onBatteryChange += ModifyBattery;
    }

    public void ModifyHealth(float newHealth)
    {
        healthBar.fillAmount = newHealth / PlayerManager.instance.maxHealth;
        healthBar.color = gradient.Evaluate(healthBar.fillAmount);
    }

    public void ModifyBattery(float newBattery)
    {
        flashlightBar.fillAmount = newBattery / 100;
    }

    public void ModifyBreath(float newBreath)
    {
        breathBar.fillAmount = newBreath / PlayerManager.instance.maxBreath;
    }

    public void ShowMapFolleto(bool show)
    {
        folletoCanvas.enabled = show;
    }
}

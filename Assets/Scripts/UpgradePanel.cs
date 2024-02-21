using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private Base theBase;
    [SerializeField] private new Camera camera;

    [SerializeField] private bool toggled = false;
    [SerializeField] private GameObject upgradesListGameObject;
    
    [Header("Counters")]
    //Counts the number of upgrade bought for each modifier
    [SerializeField] private int nbDamageUp = 0;
    [SerializeField] private int nbSpeedUp = 0;
    [SerializeField] private int nbRangeUp = 0;
    [SerializeField] private int nbRevenueUp = 0;
    [SerializeField] private int nbHealthUp = 0;
    [SerializeField] private int nbRegenUp = 0;
    [SerializeField] private int nbReductionUp = 0;
    
    [Header("Upgrade Values")]
    //Actual value of the modifiers
    [SerializeField] private float damageValue = 1;
    [SerializeField] private float speedValue = 1;
    [SerializeField] private float rangeValue = 5;
    [SerializeField] private float revenueValue = 2;
    [SerializeField] private int healthValue = 5;
    [SerializeField] private float regenValue = 0;
    [SerializeField] private int reductionValue = 0;
    
    //The texts corresponding to the value of the modifiers
    [SerializeField] private TMP_Text damageValueText;
    [SerializeField] private TMP_Text speedValueText;
    [SerializeField] private TMP_Text rangeValueText;
    [SerializeField] private TMP_Text revenueValueText;
    [SerializeField] private TMP_Text healthValueText;
    [SerializeField] private TMP_Text regenValueText;
    [SerializeField] private TMP_Text reductionValueText;
    
    [Header("Upgrade Prices")]
    //Price to upgrade the modifiers
    [SerializeField] private int damagePrice = 5;
    [SerializeField] private int speedPrice = 5;
    [SerializeField] private int rangePrice = 10;
    [SerializeField] private int revenuePrice = 10;
    [SerializeField] private int healthPrice = 10;
    [SerializeField] private int regenPrice = 15;
    [SerializeField] private int reductionPrice = 30;

    //The texts corresponding to the price of the upgrades
    [SerializeField] private TMP_Text damagePriceText;
    [SerializeField] private TMP_Text speedPriceText;
    [SerializeField] private TMP_Text rangePriceText;
    [SerializeField] private TMP_Text revenuePriceText;
    [SerializeField] private TMP_Text healthPriceText;
    [SerializeField] private TMP_Text regenPriceText;
    [SerializeField] private TMP_Text reductionPriceText;
    


    private void Start()
    {
        UpdateUI();
        
        upgradesListGameObject.gameObject.SetActive(false);
    }

    public void ToggleVisibility()
    {
        toggled = !toggled;
        upgradesListGameObject.gameObject.SetActive(toggled);
    }

    private void UpdateUI()
    {
        damagePriceText.text = damagePrice + "";
        speedPriceText.text = speedPrice + "";
        rangePriceText.text = rangePrice + "";
        revenuePriceText.text = revenuePrice + "";
        
        healthPriceText.text = healthPrice + "";
        regenPriceText.text = regenPrice + "";

        
        damageValueText.text = damageValue + "";
        speedValueText.text = speedValue + "";
        rangeValueText.text = rangeValue + "";
        revenueValueText.text = revenueValue + "";
        
        healthValueText.text = healthValue + "";
        regenValueText.text = regenValue + "";

    }

    #region Add Functions
    
    public void AddDamage()
    {
        if (RevenueManager.Instance.money >= damagePrice)
        {
            RevenueManager.Instance.RemoveAmount(damagePrice);
            
            nbDamageUp++;
            damageValue = 1 + nbDamageUp;
            theBase.UpdateStats(damageValue, speedValue, rangeValue);
        
            damagePrice = (int)Math.Round(5 + Math.Pow(nbDamageUp, 2f));
            
            
            damagePriceText.text = damagePrice + "";
            damageValueText.text = damageValue + "";
        }
    }
    public void AddSpeed()
    {
        if (RevenueManager.Instance.money >= speedPrice)
        {
            RevenueManager.Instance.RemoveAmount(speedPrice);
            
            nbSpeedUp++;
            speedValue = 1 + 0.1f * nbSpeedUp;
            theBase.UpdateStats(damageValue, speedValue, rangeValue);

            speedPrice = (int)Math.Round(5 + Math.Pow(nbSpeedUp, 2f));
            
            
            speedPriceText.text = speedPrice + "";
            speedValueText.text = speedValue + "";
        }
    }
    public void AddRange()
    {
        if (RevenueManager.Instance.money >= rangePrice)
        {
            RevenueManager.Instance.RemoveAmount(rangePrice);
            
            nbRangeUp++;
            rangeValue = 5 + 0.1f * nbRangeUp;
            theBase.UpdateStats(damageValue, speedValue, rangeValue);

            rangePrice = (int)Math.Round(10 + Math.Pow(nbRangeUp, 3f));
            
            
            rangePriceText.text = rangePrice + "";
            rangeValueText.text = rangeValue + "";

            camera.transform.position = new Vector3(0, 0, -10f - nbRangeUp / 5f);
        }
    }
    public void AddRevenue()
    {
        if (RevenueManager.Instance.money >= revenuePrice)
        {
            RevenueManager.Instance.RemoveAmount(revenuePrice);
            
            nbRevenueUp++;
            revenueValue = 2 + nbRevenueUp;
            RevenueManager.Instance.SetModifier(nbRevenueUp);
            
            revenuePrice = (int)Math.Round(10 + Math.Pow(nbRevenueUp, 5f));
            
            
            revenuePriceText.text = revenuePrice + "";
            revenueValueText.text = revenueValue + "";
        }
    }

    public void AddHealth()
    {
        if (PermanentUpgrade.Instance.unlockedHealth && RevenueManager.Instance.money >= healthPrice)
        {
            RevenueManager.Instance.RemoveAmount(healthPrice);

            nbHealthUp++;
            healthValue = 5 + 2 * nbHealthUp;
            theBase.SetHealth(healthValue);
            
            healthPrice = (int)Math.Round(5 + Math.Pow(nbHealthUp, 2f));

            healthValueText.text = healthValue + "";
            healthPriceText.text = healthPrice + "";
        }
    }

    public void AddRegen()
    {
        if (PermanentUpgrade.Instance.unlockedHealth && RevenueManager.Instance.money >= regenPrice)
        {
            RevenueManager.Instance.RemoveAmount(regenPrice);

            nbRegenUp++;
            regenValue = 0.1f * nbRegenUp;
            theBase.SetRegen(regenValue);
            
            regenPrice = (int)Math.Round(15 + Math.Pow(nbRegenUp, 3f));

            regenValueText.text = regenValue + "";
            regenPriceText.text = regenPrice + "";
        }
    }
    
    public void AddReduction()
    {
        if (PermanentUpgrade.Instance.unlockedHealth && RevenueManager.Instance.money >= healthPrice)
        {
            RevenueManager.Instance.RemoveAmount(reductionPrice);

            nbHealthUp++;
            healthValue = 5 + 2 * nbHealthUp;
            theBase.SetHealth(healthValue);
            
            healthPrice = (int)Math.Round(5 + Math.Pow(nbHealthUp, 2f));

            healthValueText.text = healthValue + "";
            healthPriceText.text = healthPrice + "";
        }
    }
    #endregion
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private Base theBase;
    [SerializeField] private new Camera camera;

    [SerializeField] private bool toggled = false;
    [SerializeField] private GameObject upgradesListGameObject;
    
    //Counts the number of upgrade bought for each modifier
    [SerializeField] private int _nbDamageUp = 0;
    [SerializeField] private int _nbSpeedUp = 0;
    [SerializeField] private int _nbRangeUp = 0;
    [SerializeField] private int _nbRevenueUp = 0;
    
    [Header("Values")]
    //Actual value of the modifiers
    [SerializeField] private float damageValue = 1;
    [SerializeField] private float speedValue = 1;
    [SerializeField] private float rangeValue = 5;
    [SerializeField] private float revenueValue = 2;
    
    //The texts corresponding to the value of the modifiers
    [SerializeField] private TMP_Text damageValueText;
    [SerializeField] private TMP_Text speedValueText;
    [SerializeField] private TMP_Text rangeValueText;
    [SerializeField] private TMP_Text revenueValueText;

    
    [Header("Upgrades")]
    //Price to upgrade the modifiers
    [SerializeField] private int damagePrice = 5;
    [SerializeField] private int speedPrice = 5;
    [SerializeField] private int rangePrice = 10;
    [SerializeField] private int revenuePrice = 10;

    //The texts corresponding to the price of the upgrades
    [SerializeField] private TMP_Text damagePriceText;
    [SerializeField] private TMP_Text speedPriceText;
    [SerializeField] private TMP_Text rangePriceText;
    [SerializeField] private TMP_Text revenuePriceText;




    private void Start()
    {
        damagePriceText.text = damagePrice + "";
        speedPriceText.text = speedPrice + "";
        rangePriceText.text = rangePrice + "";
        revenuePriceText.text = revenuePrice + "";

        damageValueText.text = damageValue + "";
        speedValueText.text = speedValue + "";
        rangeValueText.text = rangeValue + "";
        revenueValueText.text = revenueValue + "";
        
        upgradesListGameObject.gameObject.SetActive(false);
    }

    public void ToggleVisibility()
    {
        toggled = !toggled;
        upgradesListGameObject.gameObject.SetActive(toggled);
    }

    #region Add Functions
    
    public void AddDamage()
    {
        if (RevenueManager.Instance.money >= damagePrice)
        {
            RevenueManager.Instance.RemoveAmount(damagePrice);
            
            _nbDamageUp++;
            damageValue = 1 + _nbDamageUp;
            theBase.UpdateStats(damageValue, speedValue, rangeValue);
        
            damagePrice = (int)Math.Round(5 + Math.Pow(_nbDamageUp, 2f));
            
            
            damagePriceText.text = damagePrice + "";
            damageValueText.text = damageValue + "";
        }
    }
    public void AddSpeed()
    {
        if (RevenueManager.Instance.money >= speedPrice)
        {
            RevenueManager.Instance.RemoveAmount(speedPrice);
            
            _nbSpeedUp++;
            speedValue = 1 + 0.1f * _nbSpeedUp;
            theBase.UpdateStats(damageValue, speedValue, rangeValue);

            speedPrice = (int)Math.Round(5 + Math.Pow(_nbSpeedUp, 2f));
            
            
            speedPriceText.text = speedPrice + "";
            speedValueText.text = speedValue + "";
        }
    }
    public void AddRange()
    {
        if (RevenueManager.Instance.money >= rangePrice)
        {
            RevenueManager.Instance.RemoveAmount(rangePrice);
            
            _nbRangeUp++;
            rangeValue = 5 + 0.1f * _nbRangeUp;
            theBase.UpdateStats(damageValue, speedValue, rangeValue);

            rangePrice = (int)Math.Round(10 + Math.Pow(_nbRangeUp, 3f));
            
            
            rangePriceText.text = rangePrice + "";
            rangeValueText.text = rangeValue + "";

            camera.transform.position = new Vector3(0, 0, -10f - _nbRangeUp / 5f);
        }
    }
    public void AddRevenue()
    {
        if (RevenueManager.Instance.money >= revenuePrice)
        {
            RevenueManager.Instance.RemoveAmount(revenuePrice);
            
            _nbRevenueUp++;
            revenueValue = 2 + _nbRevenueUp;
            RevenueManager.Instance.SetModifier(_nbRevenueUp);
            
            revenuePrice = (int)Math.Round(10 + Math.Pow(_nbRevenueUp, 5f));
            
            
            revenuePriceText.text = revenuePrice + "";
            revenueValueText.text = revenueValue + "";
        }
    }
    
    #endregion
    
}

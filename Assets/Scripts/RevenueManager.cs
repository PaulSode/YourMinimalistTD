using System;
using TMPro;
using UnityEngine;

public class RevenueManager : MonoBehaviour
{
    
    public static RevenueManager Instance = null;
    public int revenueModifier = 0;
    public int money = 0;

    [SerializeField] private TMP_Text moneyText;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void AddAmount(int amount)
    {
        money += amount + revenueModifier + 1;
        moneyText.text = money + "";
    }
    
    public void RemoveAmount(int amount)
    {
        money -= amount;
        moneyText.text = money + "";
    }

    public void SetModifier(int amount)
    {
        revenueModifier = amount;
    }

    public int GetMoneyRounded()
    {
        throw new NotImplementedException();
    }
}

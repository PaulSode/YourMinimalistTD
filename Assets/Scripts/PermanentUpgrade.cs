using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgrade : MonoBehaviour
{
    public static PermanentUpgrade Instance = null;
    public int permanentMoney;

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

        DontDestroyOnLoad(this);
        permanentMoney = 0;
    }

    public void AddPermanentMoeny(int money)
    {
        permanentMoney += money;
    }
}

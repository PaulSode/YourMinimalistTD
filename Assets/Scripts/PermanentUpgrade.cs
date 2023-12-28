using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgrade : MonoBehaviour
{
    public static PermanentUpgrade Instance = null;
    public int permanentMoney;
    public int maxWave;

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
        maxWave = 0;
    }

    public void AddPermanentMoney(int money)
    {
        permanentMoney += money;
        if (maxWave < money) maxWave = money;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeSceneUI : MonoBehaviour
{

    private int maxWave;

    [SerializeField] private TMP_Text maxWaveText;
    // Start is called before the first frame update
    void Start()
    {
         maxWave = PermanentUpgrade.Instance.maxWave;
         maxWaveText.text = maxWave + "";
    }

    public void NextGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void UnlockHealth()
    {
        if (PermanentUpgrade.Instance.permanentMoney >= 10)
        {
            PermanentUpgrade.Instance.permanentMoney -= 10;
            PermanentUpgrade.Instance.unlockedHealth = true;
        }
    }

    public void UnlockModifiers()
    {
        if (PermanentUpgrade.Instance.permanentMoney >= 50)
        {
            PermanentUpgrade.Instance.permanentMoney -= 50;
            PermanentUpgrade.Instance.unlockedModifiers = true;
        }
    }
}

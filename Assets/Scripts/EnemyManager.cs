using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance = null;

    public int enemyCount;
    public List<GameObject> enemyList;
    [SerializeField] private GameObject enemyToSpawn;
    
    public int waveCount;
    public bool waveStarted;
    
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
        enemyList = new List<GameObject>();
        enemyCount = 0;
        waveCount = 1;
        
    }

    private void Start()
    {
       StartCoroutine(StartWave());
    }

    void Update()
    {
        if (waveStarted && enemyList.Count < 1)
        {
            StartCoroutine(StartWave());
        }
    }

    private IEnumerator StartWave()
    {
        waveStarted = false;
        
        enemyCount = waveCount * 3;
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyToSpawn);
            enemy.gameObject.GetComponent<Enemy>().mHp = Math.Pow(1.1f, waveCount - 3);
            var vector2 = Random.insideUnitCircle.normalized * 10;
            enemy.transform.localPosition = new Vector2(vector2.x, vector2.y);
            enemyList.Add(enemy);
            
            float delay = Random.Range(0.2f - (-1/(waveCount+5) + 0.21f), -1/waveCount + 1.01f);
            yield return new WaitForSeconds(delay);
        }

        waveCount++;
        waveStarted = true;
    }
}

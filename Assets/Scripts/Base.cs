using System;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private AudioSource _shootSound;
    
    
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text currentHealthText;
    [SerializeField] private TMP_Text maxHealthText;
    

    [SerializeField] private float currentHp = 100;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float regenRate = 0;
    [SerializeField] private float damageReduction = 0;

    [Header("Attack")]
    [SerializeField] private float damage = 1;
    [SerializeField] private float attackSpeed = 1;
    [SerializeField] private float range = 5;
    [SerializeField] private int piercing = 1;
    [SerializeField] private int division = 0;
    
    [SerializeField] private GameObject bulletToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject rangeIndicator;

    private float cd = 1;
    private float regenCd = 1;
    private Transform _closest;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _shootSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        rangeIndicator.transform.localScale = new Vector2(2 * range, 2 * range);
    }

    void Update()
    {
        if (cd <= 0)
        {
            _closest = FindClosestEnemy();
            if (_closest != null)
            {
                Face(_closest);
                ShootAt(_closest);
                cd = 1 / attackSpeed;
            }
        }

        if (regenCd <= 0 && currentHp < maxHp)
        {
            currentHp = Math.Min(maxHp, (float)Math.Round(currentHp + regenRate, 2));
            regenCd = 1;
            UpdateHealthUI();
        }

        regenCd -= Time.deltaTime;
        cd -= Time.deltaTime;
    }

    #region Shoot
    
    private Transform FindClosestEnemy()
    {
        List<GameObject> enemies = EnemyManager.Instance.enemyList;
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(this.transform.localPosition, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        if (closestEnemy != null && closestDistance <= range)
        {
            return closestEnemy.transform;
        }
        else
        {
            return null;
        }
    }

    private void Face(Transform target)
    {
        Vector2 direction = (transform.localPosition - target.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void ShootAt(Transform target)
    {
        var bullet = Instantiate(bulletToSpawn);
        bullet.transform.position = spawnPoint.transform.position;
        bullet.transform.rotation = spawnPoint.transform.rotation;
        bullet.gameObject.GetComponent<Bullet>().damage = this.damage;
        _shootSound.Play();
    }
    #endregion
    
    public void UpdateStats(float dmg, float spd, float rng)
    {
        damage = dmg;
        attackSpeed = spd;
        range = rng;

        rangeIndicator.transform.localScale = new Vector2(2 * range, 2 * range);
    }
    
    public void UpdateHealthUI()
    {
        healthBar.transform.localScale = new Vector3(currentHp / maxHp, 1, 1);
        currentHealthText.text = currentHp + "";
        maxHealthText.text = maxHp + "";
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            currentHp -= other.gameObject.GetComponentInChildren<Enemy>().GetDamage() - damageReduction;
            EnemyManager.Instance.enemyList.Remove(other.gameObject);
            EnemyManager.Instance.enemyCount--;
            Destroy(other.transform.parent.gameObject);
            if (currentHp <= 0)
            {
                EnemyManager.Instance.EndGame();
            }

            UpdateHealthUI();
        }
    }

    public void SetHealth(int value)
    {
        
        currentHp = currentHp + value - maxHp;
        maxHp = value;
        
        UpdateHealthUI();
    }

    public void SetRegen(float value)
    {
        regenRate = value;
    }

    public void SetReduction(float value)
    {
        damageReduction = value;
    }

}
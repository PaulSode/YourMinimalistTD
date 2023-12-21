using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Base : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float currentHp = 100;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float regenRate = 1;

    [SerializeField] private float damage = 1;
    [SerializeField] private float attackSpeed = 1;
    [SerializeField] private float range = 5;

    [SerializeField] private GameObject bulletToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject rangeIndicator;

    private float cd = 0;
    private Transform _closest;

    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
       
    }
    
    private void Start()
    {
        rangeIndicator.transform.localScale = new Vector2(2*range, 2*range);
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
                cd = 1/attackSpeed;
            }
        }

        if (currentHp < maxHp)
        {
            currentHp = Mathf.Min(maxHp, currentHp + regenRate * Time.deltaTime);
        }

        cd -= Time.deltaTime;
    }

    private Transform FindClosestEnemy()
    {
        
        List<GameObject> enemies = EnemyManager.Instance.enemyList;
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
 
        foreach (GameObject enemy in enemies) {
            float distance = Vector2.Distance(this.transform.localPosition, enemy.transform.position);
 
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        if (closestEnemy != null && closestDistance <= range)
        {
            Debug.Log(closestDistance);
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

    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            currentHp -= 10;
            EnemyManager.Instance.enemyList.Remove(other.gameObject);
            EnemyManager.Instance.enemyCount--;
            Destroy(other.gameObject);
            
        }
    }
}
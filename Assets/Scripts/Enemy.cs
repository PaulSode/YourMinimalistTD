using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public double hp;
    public double mHp = 3;

    [SerializeField] private float damage = 1;
    [SerializeField] private float speed = 1;
    private Vector2 objective = Vector2.zero;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
       
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        hp = mHp;
        _rigidbody2D.AddTorque(Random.Range(-1f, 1f));
    }

    void Update()
    {
        var distance = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, objective, distance);
        
    }
    
    public void RemoveHP(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            EnemyManager.Instance.enemyList.Remove(gameObject);
            EnemyManager.Instance.enemyCount--;
            RevenueManager.Instance.AddAmount(1);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}

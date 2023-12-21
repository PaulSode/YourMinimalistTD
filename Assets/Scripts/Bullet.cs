using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 2;
    [SerializeField] private float lifepsan = 4;
    [SerializeField] private Vector2 velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
       
       _rb.velocity = -transform.up * speed;
        lifepsan -= Time.deltaTime;
        velocity = _rb.velocity;
        if (lifepsan <= 0)
        {
            Destroy(gameObject);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            EnemyManager.Instance.enemyList.Remove(other.gameObject);
            EnemyManager.Instance.enemyCount--;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

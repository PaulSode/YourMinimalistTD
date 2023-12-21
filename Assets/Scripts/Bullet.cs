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
    public float damage = 1;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().RemoveHP(damage);
            Destroy(gameObject);
        }
    }
}

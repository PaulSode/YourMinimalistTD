using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 2;
    [SerializeField] private float lifepsan = 4;
    [SerializeField] private Vector2 velocity;
    
    
    public float damage = 1;

    [SerializeField] private List<GameObject> piercedEnemies;
    public int pierce = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        piercedEnemies = new List<GameObject>();
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
        if (other.gameObject.name.Contains("Enemy") && !piercedEnemies.Contains(other.gameObject))
        {
            other.gameObject.GetComponent<Enemy>().RemoveHP(damage);
            pierce -= 1;
            if (pierce == 0)
            {
                Destroy(gameObject);    
            }
            piercedEnemies.Add(other.gameObject);
        }
    }
}

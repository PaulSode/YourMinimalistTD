using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float mHp = 100;

    [SerializeField] private float damage = 1;
    [SerializeField] private float speed = 1;
    private Vector2 objective = Vector2.zero;

    private void Awake()
    {
        hp = 100;
    }

    void Update()
    {
        var distance = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, objective, distance);
    }
}

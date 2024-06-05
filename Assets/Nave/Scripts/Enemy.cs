using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    public BoxCollider2D enemyCollisor;
    private float velocity;

    void Start()
    {
        velocity = Random.Range(-1, -5);
    }
    public void FixedUpdate()
    {
        rbEnemy.velocity = new Vector2(0, velocity);
    }
}

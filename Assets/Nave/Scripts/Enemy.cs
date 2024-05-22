using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    public BoxCollider2D enemyCollisor;
    public float velocity;
    
    public void FixedUpdate()
    {
        rbEnemy.velocity = new Vector2(0, velocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D rbBullet;
    public BoxCollider2D shotCollisor;
    public float velocity;
    public Transform startExplosion;
    public GameObject explosion;
    public float enemyDeaths;
    public float pontuation;

    void Update()
    {
        enemyDeaths = pontuation;
    }
    public void FixedUpdate()
    {
        rbBullet.velocity = new Vector2(0, velocity);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
            var copy = Instantiate(explosion, startExplosion.position, Quaternion.identity);
            Destroy(copy, .5f);
            Player.instance.enemyDeaths++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{   
    public static Player instance;


    public GameObject player;
    public Rigidbody2D rb;
    public BoxCollider2D collisor;
    public float inputX;
    public float inputY;
    public float velocity;
    public bool canMoveX;
    public bool canMoveY;
    public float inputShot;
    public bool canShot;
    public float fireRate;
    public GameObject playerShot;
    public Transform playerAim;
    public float playerLife;
    public GameObject telaMorrer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerLife = 10;
        telaMorrer.SetActive(false);
    }

    void Update()
    {
        if(canMoveX)
            inputX = Input.GetAxis("Horizontal");
        if(canMoveY)
            inputY = Input.GetAxis("Vertical");

        inputShot = Input.GetAxis("Fire1");

        if(inputShot != 0)
            Shot();

        if (playerLife == 0)
        {
            Morrer();
        }
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * velocity, inputY * velocity);
    }

    public void Shot()
    {
        if(canShot)
        {
            StartCoroutine(ShotProjectile());
        }
    }

    public IEnumerator ShotProjectile()
    {
        canShot = false;
        Instantiate(playerShot, playerAim.position, Quaternion.identity);
        yield return new WaitForSeconds(fireRate);
        canShot = true;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            playerLife--;
        }
    }

    public void Morrer()
    {
        player.SetActive(false);
        telaMorrer.SetActive(true);
        GameManager.instance.enemyObject.SetActive(false);
    }
}

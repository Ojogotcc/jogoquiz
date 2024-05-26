using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{   
    public static Player instance;

    [Header("Movimentação")]    
    public GameObject player;
    public Rigidbody2D rb;
    public BoxCollider2D collisor;
    public float inputX, inputY, velocity; 
    public bool canMoveX, canMoveY ;

    [Header("TiroBasico")]
    public float inputShot, fireRate;
    public bool canShot;
    public GameObject playerShot, telaMorrer ;
    public Transform playerAim;
    public float playerLife;

    [Header("Raio")]
    public Transform[] playerAim2;
    public float inputShot2;
    public GameObject[] playerBeam;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerLife = 10;
        telaMorrer.SetActive(false);

        GameManager.instance.enemyObject[GameManager.instance.enemyGenerator.Length].SetActive(true);
    }

    void Update()
    {
        if(canMoveX)
            inputX = Input.GetAxis("Horizontal");
        if(canMoveY)
            inputY = Input.GetAxis("Vertical");

        inputShot = Input.GetAxis("Fire1");

        inputShot2 = Input.GetAxis("Fire2");

        if(inputShot != 0)
            Shot();

        if(inputShot2 != 0)
            Shot2();
        
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
            Morrer();
        }
    }

    public void Morrer()
    {
        player.SetActive(false);
        telaMorrer.SetActive(true);
        GameManager.instance.enemyObject[GameManager.instance.enemyGenerator.Length].SetActive(true);
    }

    public void Shot2()
    {
        if(canShot)
            StartCoroutine(ShotProjectile2());
    }

    public IEnumerator ShotProjectile2()
    {
        canShot = false;
        Instantiate(playerBeam[0], playerAim2[0].position, Quaternion.identity);
        Instantiate(playerBeam[1], playerAim2[1].position, Quaternion.identity);
        yield return new WaitForSeconds(fireRate);
        canShot = true;
    }
}

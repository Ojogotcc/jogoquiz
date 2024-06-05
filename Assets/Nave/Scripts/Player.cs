using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Movimentação")]
    public GameObject player;
    public Rigidbody2D rb;
    public BoxCollider2D collisor;
    public float inputX, inputY, velocity;
    public bool canMoveX, canMoveY;

    [Header("TiroBasico")]
    public float inputShot, fireRate;
    public bool canShot;
    public GameObject playerShot, telaMorrer, buttons;
    public Transform playerAim;
    public float playerLife;

    [Header("Raio")]
    public Transform[] playerAim2;
    public float inputShot2;
    public GameObject[] playerBeam;
    public int enemyDeaths;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerLife = 1;
        enemyDeaths = 0;
        telaMorrer.SetActive(false);
        GameManager.instance.enemyObject[GameManager.instance.enemyGenerator.Length].SetActive(true);
    }

    void Update()
    {
        if (canMoveX)
            inputX = Input.GetAxis("Horizontal");

        if(canMoveY)
            inputY = Input.GetAxis("Vertical");

        if (playerLife == 0)
        {
            Morrer();
        }

        inputShot = Input.GetAxis("Fire1");
        inputShot2 = Input.GetAxis("Fire2");

        if(inputShot != 0)
            Shot();
        
        if(inputShot2 != 0)
            Shot2();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * velocity, inputY * velocity);
    }


    public void Shot()
    {
        if (canShot)
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

    public void PerderTamanho()
    {
        if(playerLife > 1)
        {
            player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    void Morrer()
    {
        telaMorrer.SetActive(true);
        player.SetActive(false);
        buttons.SetActive(false);
    }

    public void Reinitialize()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("Enemy"))
        {
            playerLife--;
        }

        if (playerLife < 3)
        {
            if (collider.CompareTag("DaVelocidade"))
            {
                playerLife += 1;
                Destroy(collider.gameObject);
                if (playerLife == 2)
                {
                    player.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
                }
            }
        }

        if (collider.CompareTag("AumentaoRange"))
        {
            StartCoroutine(AumentaroRange());
            Destroy(collider.gameObject);
        }
    }

    IEnumerator AumentaroRange()
    {
        fireRate += .2f;
        yield return new WaitForSeconds(45);
        fireRate = .1f;
    }


    public void Shot2()
    {
        if (canShot)
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

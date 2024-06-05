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
    public bool canMove;

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

    [Header("Vida")]
    public GameObject[] playerLifeImg;
    public Transform[] lifeAnchor;

    public GameObject vida;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerLife = 1;
        telaMorrer.SetActive(false);
        GameManager.instance.enemyObject[GameManager.instance.enemyGenerator.Length].SetActive(true);
    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Moved)
            {
                transform.position += (Vector3)t.deltaPosition / 600;
            }
        }

        if (playerLife == 0)
        {
            Morrer();
        }
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

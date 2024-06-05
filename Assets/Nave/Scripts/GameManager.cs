using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] enemyObject, scenes, powerUps;
    public Transform[] enemyGenerator, powerUpsGenerator;
    public float enemyRange;
    public float powerUpsRange;
    public GameObject player, buttons;
    public TMP_Text pontuacao;

    public string textoPontuacao;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(GenerateEnemy());
        StartCoroutine(GeneratePowerUps());
        buttons.SetActive(false);
        player.SetActive(false);
        enemyObject[enemyObject.Length].SetActive(false);
        scenes[0].SetActive(true);
        scenes[1].SetActive(false);
    }

    public void startGame()
    {
        scenes[0].SetActive(false);
        buttons.SetActive(true);
        buttons.SetActive(true);
        player.SetActive(true);
        enemyObject[enemyObject.Length].SetActive(true);

    }

    public IEnumerator GenerateEnemy()
    {
        int rnd = Random.Range(0, enemyGenerator.Length);
        int rndG = Random.Range(0, enemyObject.Length);
        Instantiate(enemyObject[rndG], enemyGenerator[rnd].position, Quaternion.identity);
        yield return new WaitForSeconds(enemyRange);
        StartCoroutine(GenerateEnemy());
    }

    public IEnumerator GeneratePowerUps()
    {
        int rnd = Random.Range(0, powerUpsGenerator.Length);
        int rndG = Random.Range(0, powerUps.Length);
        Instantiate(powerUps[rndG], powerUpsGenerator[rnd].position, Quaternion.identity);
        yield return new WaitForSeconds(powerUpsRange);
        StartCoroutine(GeneratePowerUps());

    }

    public void GenerateScene()
    {
        StartCoroutine(GenerateEnemy());
        StartCoroutine(GeneratePowerUps());
        buttons.SetActive(false);
        player.SetActive(false);
        enemyObject[enemyObject.Length].SetActive(false);
        scenes[0].SetActive(true);
        scenes[1].SetActive(false);
    }

    void PegarPontuacao()
    {
        pontuacao.text = $"Score {Player.instance.enemyDeaths}";
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject enemyObject;
    public Transform[] enemyGenerator;
    public float enemyRange;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(GenerateEnemy());
    }

    public IEnumerator GenerateEnemy()
    {
        int rnd = Random.Range(0, enemyGenerator.Length);
        Instantiate(enemyObject, enemyGenerator[rnd].position, Quaternion.identity);
        yield return new WaitForSeconds(enemyRange);
        StartCoroutine(GenerateEnemy());
    }

    public void GenerateScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

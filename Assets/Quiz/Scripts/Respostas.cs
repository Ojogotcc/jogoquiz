using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Accessibility;


public class Respostas : MonoBehaviour
{   
    [Header("Panels")]
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject endPanel;
    
    [Header("GameObjects")]
    public TMP_Text textTitle;
    public TMP_Text textQuestion;
    public Image imageQuiz;
    public TMP_Text[] textAnswer;

    [Header("Sounds")]
    public AudioSource MusicBox;
    public AudioSource EffectBox;

    [Header("Musiques Archives")]
    public AudioClip musicMenu;
    public AudioClip gameMusic;

    [Header("Sound Effects Archives")]
    public AudioClip correctEffect;
    public AudioClip wrongEffect;
    
    [Header("Arrays")]
    public string[] titles;
    public Sprite[] images;
    public string[] questions;
    public string[] alternative1;
    public string[] alternative2;
    public string[] alternative3;
    public string[] alternative4;
    public int[] correctAnswer;
    public int actualQuestion;
    
    void Start()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);

        MusicBox.clip = musicMenu;
        MusicBox.Play();


    }

    public void Initialize()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        gamePanel.SetActive(true);
        NextQuestion(actualQuestion);
        MusicBox.clip = gameMusic;
        MusicBox.Play();
    }
    // Initialize game method



    public void NextQuestion(int number)
    {
        textTitle.text = titles[number];
        textQuestion.text = questions[number];
        imageQuiz.sprite = images[number];
        textAnswer[0].text = alternative1[number];
        textAnswer[1].text = alternative2[number];
        textAnswer[2].text = alternative3[number];
        textAnswer[3].text = alternative4[number];
    }

    public void CheckAnswer(int number)
    {
        
        StartCoroutine(ValidateAnswer(number));
        
    }

    IEnumerator ValidateAnswer(int number)
    {
        if(number == correctAnswer[actualQuestion])
        {
            textAnswer[number].GetComponentInParent<Button>().interactable = false;
            imageQuiz.color = Color.green;
            EffectBox.PlayOneShot(correctEffect);   
            actualQuestion++;
             textAnswer[number].GetComponentInParent<Button>().interactable = true;
           
        }
            
        else 
        {
            imageQuiz.color = Color.red;
            EffectBox.PlayOneShot(wrongEffect);
            yield return new WaitForSeconds(1);
            startPanel.SetActive(true);
            gamePanel.SetActive(false);
            actualQuestion = 0; 
            MusicBox.clip = musicMenu;
            MusicBox.Play();
        }

        yield return new WaitForSeconds(1);

        imageQuiz.color = Color.white;  
        if(actualQuestion >= titles.Length)
        {
            gamePanel.SetActive(false);
            startPanel.SetActive(false);
            endPanel.SetActive(true);
            actualQuestion = 0;
            MusicBox.clip = musicMenu;
            MusicBox.Play();
        }
        else
        {
            NextQuestion(actualQuestion);
        }
         
    }
    
    public void restart()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    }


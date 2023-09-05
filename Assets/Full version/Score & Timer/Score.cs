using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Wave")]
    [SerializeField] EnemySpawner Spawner;
    [SerializeField] TextMeshProUGUI waveState;

    [Header("Score")]
    [SerializeField] int score;
    [SerializeField] int numOfScores;
    [SerializeField] Image[] scores;
    [SerializeField] Sprite fullScore;
    [SerializeField] Sprite emptyHeart;

    [Header("Time")]
    [SerializeField] GameObject[] timer = new GameObject[2];
    [SerializeField] int firstScore = 0;
    [SerializeField] int secondScore = 0;
    [SerializeField] private GameObject screenManager;
    [SerializeField] private GameObject credit;
    [SerializeField] TextMeshProUGUI textMeshProUGUI1;
    [SerializeField] TextMeshProUGUI textMeshProUGUI2;
    PlayerHealther playerHealther;

    private void Update()
    {
        if (screenManager.GetComponent<ScreenChange>().CurrentScreenNumber == 2)
        {
            score = 4 - firstScore;

            for (int i = 0; i < 4; i++)
            {
                scores[i] = GameObject.Find("Score Canvas").transform.GetChild(i).GetComponent<Image>();
            }

            waveState = GameObject.Find("Score Canvas").transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();  
        }
        else if (screenManager.GetComponent<ScreenChange>().CurrentScreenNumber == 4)
        {
            score = 4 - secondScore;

            for (int i = 0; i < 4; i++)
            {
                scores[i] = GameObject.Find("Score Canvas").transform.GetChild(i).GetComponent<Image>();
            }

            waveState = GameObject.Find("Score Canvas").transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        if (score>numOfScores) { 
            score = numOfScores;
        }

        for (int i = 0; i < scores.Length; i++) 
        {
            if (i < score) {
                scores[i].sprite = fullScore;
            }else {
                scores[i].sprite = emptyHeart;
            }

            if(i<numOfScores) {
                scores[i].enabled = true;
            } else {
                scores[i].enabled = false;  
            }
        }

        if (screenManager.GetComponent<ScreenChange>().CurrentScreenNumber == 6) 
        {
            credit = GameObject.Find("Credit");

            if (credit != null)
            {
                if (firstScore <= 2 && secondScore <= 2)
                {
                    credit.GetComponent<Credit>().choice = 1;
                }
                else if (firstScore > 2 && secondScore <= 2)
                {
                    credit.GetComponent<Credit>().choice = 2;
                }
                else if (firstScore <= 2 && secondScore > 2)
                {
                    credit.GetComponent<Credit>().choice = 3;
                }
                else if (firstScore > 2 && secondScore > 2)
                {
                    credit.GetComponent<Credit>().choice = 4;
                }
            }
        }

        if (screenManager.GetComponent<ScreenChange>().CurrentScreenNumber == 2 || screenManager.GetComponent<ScreenChange>().CurrentScreenNumber == 4)
        {
            playerHealther = GameObject.Find("Player").GetComponent<PlayerHealther>();
            Spawner = GameObject.Find("Spawner").GetComponent<EnemySpawner>();
            waveState.text = "Wave " + (Spawner.nextWave + 1) + "/4";

            if (playerHealther != null && !playerHealther.isDead) 
            {
                firstScore = timer[0].GetComponent<Timer>().firstTimeOverCount;
                secondScore = timer[1].GetComponent<Timer>().secondTimeOverCount;
            }
        }

        if (firstScore <= 2)
        {
            textMeshProUGUI1.text =
                "The result of four waves, \r\nYour simulation at the moment is \r\nThe success rate of wave \r\nHigher than the failure rate.";
        }
        else 
        {
            textMeshProUGUI1.text =
                "The result of four waves, \r\nYour simulation at the moment is \r\nThe success rate of wave \r\nLower than the failure rate.";
        }

        if (secondScore <= 2)
        {
            textMeshProUGUI2.text =
                "The result of four waves, \r\nYour simulation at the moment is \r\nThe success rate of wave \r\nHigher than the failure rate.";
        }
        else
        {
            textMeshProUGUI2.text =
                "The result of four waves, \r\nYour simulation at the moment is \r\nThe success rate of wave \r\nLower than the failure rate.";
        }
    }

    public void ResetScore1()
    {
        timer[0].GetComponent<Timer>().firstTimeOverCount = 0;
    }

    public void ResetScore2()
    {
        timer[1].GetComponent<Timer>().secondTimeOverCount = 0;
    }
}

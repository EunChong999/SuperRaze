using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EnemySpawner;

public class Timer : MonoBehaviour
{
    private float timeDuration = 30f;

    [SerializeField]
    private bool coolDown = true;

    private bool isTimeOver = false;

    private float timer;

    [SerializeField] public int firstTimeOverCount;
    [SerializeField] public int secondTimeOverCount;

    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;

    private float flashTimer;
    private float flashDuration = 1f;

    EnemySpawner enemySpawner;
    [SerializeField] private ScreenTimer screenTimer;
    GameObject player;
    [SerializeField] private GameObject screenManager;

    // Start is called before the first frame update
    void Start()
    {
        firstTimeOverCount = 0;
        secondTimeOverCount = 0;
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawner == null)
        {
            enemySpawner = GameObject.Find("Spawner").GetComponent<EnemySpawner>();
        }

        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        if (enemySpawner.currentState == EnemySpawner.SpawnState.COUNTING)
        {
            if (!enemySpawner.isWaveCompleted)
            {
                ResetTimer();
            }
        }
        else
        {
            if (!screenTimer.isTimeStop &&
           !player.GetComponent<PlayerHealther>().isDead &&
           !player.GetComponent<PlayerHealther>().isDissolving &&
           enemySpawner.currentState != EnemySpawner.SpawnState.SPAWNING &&
           !screenManager.GetComponent<ScreenChange>().on)
            {
                if (coolDown && timer > 0)
                {
                    timer -= Time.deltaTime;
                    UpdateTimeDisplay(timer);
                }
                else if (!coolDown && timer < timeDuration)
                {
                    timer += Time.deltaTime;
                    UpdateTimeDisplay(timer);
                }
                else
                {
                    Flash();

                    if (screenManager.GetComponent<ScreenChange>().CurrentScreenNumber == 2 && !isTimeOver)
                    {
                        firstTimeOverCount++;
                        isTimeOver = true;
                    }
                    else if (screenManager.GetComponent<ScreenChange>().CurrentScreenNumber == 4 && !isTimeOver)
                    {
                        secondTimeOverCount++;
                        isTimeOver = true;
                    }

                }
            }
        }
    }



    public void ResetTimer()
    {
        isTimeOver = false;

        if (coolDown)
        {
            timer = timeDuration;
        }
        else
        {
            timer = 0;
        }
        SetTextDisplay(true);
    }

    private void UpdateTimeDisplay(float time)
    {
        if (time < 0)
        {
            time = 0;    
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private void Flash()
    {
        if(coolDown && timer != 0)
        {
            timer = 0;
            UpdateTimeDisplay(timer);
        }

        if (!coolDown && timer != timeDuration)
        {
            timer = 0;
            UpdateTimeDisplay(timer);
        }

        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }
        else if(flashTimer >= flashDuration / 2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }
        else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
    }

    private void SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        separator.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;
    }
}

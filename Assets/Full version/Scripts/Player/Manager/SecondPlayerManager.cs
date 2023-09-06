using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    private EnemySpawner enemySpawner;
    [SerializeField] private PlayerTimeRewinder playerTimeRewinder;
    [SerializeField] private PlayerHealther playerHealther;
    [SerializeField] private GameObject collisionCheker;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
        enemySpawner = GameObject.Find("Spawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealther.isDead && !playerHealther.isDissolving)
        {
            gameObject.GetComponent<PlayerHealther>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<PlayerHealther>().enabled = true;
        }

        if (screenTimer.isTimeStop || playerHealther.isDissolving || playerHealther.isDead || enemySpawner.isWaveCompleted)
        {
            transform.GetChild(0).gameObject.layer = 0;
            collisionCheker.SetActive(false);
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<SecondPlayerController>().enabled = false;
            gameObject.GetComponent<SecondPlayerShooter>().enabled = false;

            if (!gameObject.GetComponent<PlayerTimeRewinder>().isRewinding)
            {
                gameObject.GetComponent<PlayerTimeRewinder>().enabled = false;
            }

            gameObject.GetComponent<AfterImage>().enabled = false;
            gameObject.GetComponent<AfterImage>().makeImage = false;
        }
        else
        {
            transform.GetChild(0).gameObject.layer = 7;
            collisionCheker.SetActive(true);
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<SecondPlayerController>().enabled = true;
            gameObject.GetComponent<PlayerTimeRewinder>().enabled = true;

            gameObject.GetComponent<AfterImage>().enabled = true;

            if (playerTimeRewinder.isRewinding)
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.layer = 0;
                gameObject.GetComponent<SecondPlayerShooter>().enabled = false;
                gameObject.GetComponent<SecondPlayerController>().enabled = false;
            }
            else
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.layer = 7;
                gameObject.GetComponent<SecondPlayerShooter>().enabled = true;
                gameObject.GetComponent<SecondPlayerController>().enabled = true;
            }
        }
    }
}

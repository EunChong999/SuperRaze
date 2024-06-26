using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrackingEnemyManager : MonoBehaviour, IPooledObject
{
    private ScreenTimer screenTimer;
    [SerializeField] EnemyHealther enemyHealther;
    private TrackingEnemyHealther trackingEnemyHealther;
    private TrackingEnemyChaser trackingEnemyChaser;
    [SerializeField] private GameObject collisionCheck;
    private PlayerTimeRewinder playerTimeRewinder;

    public void OnObjectSpawn(ScreenTimer timer)
    {
        screenTimer = timer;
        enemyHealther.screenTimer = timer;
        trackingEnemyHealther = gameObject.GetComponent<TrackingEnemyHealther>();
        trackingEnemyChaser = gameObject.GetComponent<TrackingEnemyChaser>();
        playerTimeRewinder = GameObject.Find("Player").GetComponent<PlayerTimeRewinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackingEnemyHealther.isDead)
        {
            trackingEnemyChaser.enabled = false;
            collisionCheck.SetActive(false);

            if (!trackingEnemyHealther.isDissolving)
            {
                gameObject.SetActive(false);
            }
        }

        if (screenTimer.isTimeStop || trackingEnemyHealther.isDissolving || trackingEnemyHealther.isDead || playerTimeRewinder.isRewinding)
        {
            transform.GetChild(0).GetChild(0).GetComponent<TrackingEnemyCollisionChecker>().enabled = false;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<TrackingEnemyChaser>().enabled = false;
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<TrackingEnemyCollisionChecker>().enabled = true;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<TrackingEnemyChaser>().enabled = true;
        }
    }
}

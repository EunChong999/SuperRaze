using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrackingEnemyManager : MonoBehaviour, IPooledObject
{
    private ScreenTimer screenTimer;
    private TrackingEnemyHealther trackingEnemyHealther;
    private TrackingEnemyChaser trackingEnemyChaser;
    [SerializeField] private GameObject collisionCheck;

    public void OnObjectSpawn(ScreenTimer timer)
    {
        screenTimer = timer;
        trackingEnemyHealther = gameObject.GetComponent<TrackingEnemyHealther>();
        trackingEnemyChaser = gameObject.GetComponent<TrackingEnemyChaser>();
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

        if (screenTimer.isTimeStop || trackingEnemyHealther.isDissolving || trackingEnemyHealther.isDead)
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

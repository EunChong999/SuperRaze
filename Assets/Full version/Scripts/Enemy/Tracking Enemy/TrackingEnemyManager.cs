using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemyManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    private TrackingEnemyHealther trackingEnemyHealther;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
        trackingEnemyHealther = gameObject.GetComponent<TrackingEnemyHealther>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackingEnemyHealther.isDead && !trackingEnemyHealther.isDissolving)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
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

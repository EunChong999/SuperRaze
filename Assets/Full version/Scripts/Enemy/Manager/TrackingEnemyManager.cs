using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemyManager : MonoBehaviour
{
    private ScreenTimer screenTimer;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (screenTimer.isTimeStop)
        {
            transform.GetChild(0).GetChild(0).GetComponent<EnemyCollisionChecker>().enabled = false;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<TrackingEnemyChaser>().enabled = false;
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<EnemyCollisionChecker>().enabled = true;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<TrackingEnemyChaser>().enabled = true;
        }
    }
}

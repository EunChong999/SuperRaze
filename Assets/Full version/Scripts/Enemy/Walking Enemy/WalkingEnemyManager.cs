using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    private WalkingEnemyHealther walkingEnemyHealther;
    private WalkingEnemyPatroller walkingEnemyPatroller;
    [SerializeField] private GameObject collisionCheck;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
        walkingEnemyHealther = gameObject.GetComponent<WalkingEnemyHealther>();
        walkingEnemyPatroller = gameObject.GetComponent<WalkingEnemyPatroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walkingEnemyHealther.isDead)
        {
            walkingEnemyPatroller.enabled = false;
            collisionCheck.SetActive(false);

            if (!walkingEnemyHealther.isDissolving)
            {
                gameObject.SetActive(false);
            }
        }

        if (screenTimer.isTimeStop || walkingEnemyHealther.isDissolving || walkingEnemyHealther.isDead)
        {
            transform.GetChild(0).GetChild(0).GetComponent<WalkingEnemyCollisionChecker>().enabled = false;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<WalkingEnemyPatroller>().enabled = false;
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<WalkingEnemyCollisionChecker>().enabled = true;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<WalkingEnemyPatroller>().enabled = true;
        }
    }
}

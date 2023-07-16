using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooingEnemyManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    private ShootingEnemyHealther shootingEnemyHealther;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
        shootingEnemyHealther = gameObject.GetComponent<ShootingEnemyHealther>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingEnemyHealther.isDead && !shootingEnemyHealther.isDissolving)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        if (screenTimer.isTimeStop || shootingEnemyHealther.isDissolving || shootingEnemyHealther.isDead)
        {
            transform.GetChild(0).GetChild(0).GetComponent<ShootingEnemyCollisionChecker>().enabled = false;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<ShooingEnemyThrower>().enabled = false;
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<ShootingEnemyCollisionChecker>().enabled = true;
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<ShooingEnemyThrower>().enabled = true;
        }
    }
}

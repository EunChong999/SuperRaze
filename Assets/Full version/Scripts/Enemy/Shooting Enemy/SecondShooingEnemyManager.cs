using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondShooingEnemyManager : MonoBehaviour, IPooledObject
{
    private ScreenTimer screenTimer;
    private ShootingEnemyHealther shootingEnemyHealther;
    private ShooingEnemyThrower shooingEnemyThrower;
    [SerializeField] private GameObject collisionCheck;
    private PlayerTimeRewinder playerTimeRewinder;

    public void OnObjectSpawn()
    {
        screenTimer = FindObjectOfType<ScreenTimer>();
        shootingEnemyHealther = gameObject.GetComponent<ShootingEnemyHealther>();
        shooingEnemyThrower = gameObject.GetComponent<ShooingEnemyThrower>();
        playerTimeRewinder = GameObject.Find("Player").GetComponent<PlayerTimeRewinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingEnemyHealther.isDead)
        {
            shooingEnemyThrower.enabled = false;
            collisionCheck.SetActive(false);

            if (!shootingEnemyHealther.isDissolving)
            {
                gameObject.SetActive(false);
            }
        }

        if (screenTimer.isTimeStop || shootingEnemyHealther.isDissolving || shootingEnemyHealther.isDead || playerTimeRewinder.isRewinding)
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

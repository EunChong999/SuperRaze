using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingEnemyManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    private BombingEnemyExploder bombingEnemyExploder;
    private BombingEnemyHealther bombingEnemyHealther;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
        bombingEnemyExploder = gameObject.GetComponent<BombingEnemyExploder>();
        bombingEnemyHealther = gameObject.GetComponent<BombingEnemyHealther>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bombingEnemyHealther.isDead && !bombingEnemyHealther.isDissolving)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        if (screenTimer.isTimeStop || bombingEnemyHealther.isDissolving || bombingEnemyHealther.isDead)
        {
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<BombingEnemyExploder>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BombingEnemyExploder>().enabled = true;

            if (bombingEnemyExploder.isBombing)
            {
                transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                transform.GetChild(0).GetComponent<Animator>().enabled = false;
            }
            else
            {
                transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
        }
    }
}

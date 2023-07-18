using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBombingEnemyManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    private BombingEnemyHealther bombingEnemyHealther;
    private BombingEnemyExploder bombingEnemyExploder;
    [SerializeField] private GameObject collisionCheck;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
        bombingEnemyHealther = gameObject.GetComponent<BombingEnemyHealther>();
        bombingEnemyExploder = gameObject.GetComponent<BombingEnemyExploder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bombingEnemyHealther.isDead)
        {
            bombingEnemyExploder.enabled = false;
            collisionCheck.SetActive(false);

            if (!bombingEnemyHealther.isDissolving)
            {
                gameObject.SetActive(false);
            }
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

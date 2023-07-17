using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    [SerializeField] private PlayerTimeSlower playerTimeSlower;
    [SerializeField] private PlayerHealther playerHealther;
    [SerializeField] private GameObject collisionCheker;

    // Start is called before the first frame update
    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
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

        if (screenTimer.isTimeStop || playerHealther.isDissolving || playerHealther.isDead) 
        {
            transform.GetChild(0).gameObject.layer = 0;
            collisionCheker.SetActive(false);
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<FirstPlayerController>().enabled = false;
            gameObject.GetComponent<FirstPlayerShooter>().enabled = false;
            gameObject.GetComponent<PlayerTimeSlower>().enabled = false;
            gameObject.GetComponent<AfterImage>().enabled = false;
            gameObject.GetComponent<AfterImage>().makeImage = false;
        }
        else 
        {
            transform.GetChild(0).gameObject.layer = 7;
            collisionCheker.SetActive(true);
            transform.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<FirstPlayerController>().enabled = true;
            gameObject.GetComponent<PlayerTimeSlower>().enabled = true;
            gameObject.GetComponent<AfterImage>().enabled = true;

            if (playerTimeSlower.isSlowering)
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.layer = 0;
                gameObject.GetComponent<FirstPlayerShooter>().enabled = false;
            }
            else
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.layer = 7;
                gameObject.GetComponent<FirstPlayerShooter>().enabled = true;
            }
        }
    }
}

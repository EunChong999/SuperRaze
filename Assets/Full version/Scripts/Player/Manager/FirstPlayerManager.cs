using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FirstPlayerManager : MonoBehaviour
{
    private ScreenTimer screenTimer;
    public PhysicsMaterial2D physicsMaterial;

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
            transform.GetChild(0).GetChild(0).GetComponent<PlayerCollisionChecker>().enabled = false;
            transform.GetChild(0).GetComponent<Rigidbody2D>().sharedMaterial = null;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<PlayerShooter>().enabled = false;
            gameObject.GetComponent<PlayerTimeSlower>().enabled = false;
            gameObject.GetComponent<AfterImage>().enabled = false;
            gameObject.GetComponent<AfterImage>().makeImage = false;
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<PlayerCollisionChecker>().enabled = true;
            transform.GetChild(0).GetComponent<Rigidbody2D>().sharedMaterial = physicsMaterial;
            transform.GetChild(0).GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<PlayerController>().enabled = true;
            gameObject.GetComponent<PlayerShooter>().enabled = true;
            gameObject.GetComponent<PlayerTimeSlower>().enabled = true;
            gameObject.GetComponent<AfterImage>().enabled = true;
        }
    }
}

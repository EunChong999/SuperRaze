//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerHealther : MonoBehaviour
//{
//    public GameObject body;
//    public GameObject collisionCheck;
//    [HideInInspector] public GameObject collisionChecker;
//    [HideInInspector] public GameObject screenManager;
//    public int playerHealthCount;
//    private bool isDead;
//    private bool isRestarting;

//    // Start is called before the first frame update
//    void Start()
//    {
//        collisionChecker = GameObject.Find("Player").transform.GetChild(0).transform.GetChild(3).gameObject;
//        screenManager = GameObject.Find("Screen Manager");
//        playerHealthCount = 3;
//        isDead = false;
//        isRestarting = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(!isRestarting)
//        {
//            if (playerHealthCount - collisionChecker.GetComponent<PlayerCollisionChecker>().collisionCount <= 0)
//            {
//                body.layer = LayerMask.NameToLayer("Default");
//                collisionCheck.tag = "Untagged";
//                isDead = true;
//                isRestarting = true;
//            }
//            else
//            {
//                body.layer = LayerMask.NameToLayer("Player");
//                collisionCheck.tag = "Player";
//                isDead = false;
//            }
//        }


//        if(isDead)
//        {
//            Restart();
//            isDead = false;
//        }
//    }

//    void Restart()
//    {
//        screenManager.GetComponent<ScreenChange>().RestartScreen();
//        screenManager.GetComponent<ScreenEffect>().AffectScreen();
//        screenManager.GetComponent<ScreenBlock>().BlockScreen();
//    }
//}

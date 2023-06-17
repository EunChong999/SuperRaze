using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenEffect : MonoBehaviour
{
    public bool On;
    public bool Init;

    public GameObject EffectScreen;

    public GameObject[] Boxes;
    public Transform[] Boxes_Start_Position;
    public Transform[] Boxes_End_Position;

    public void AffectScreen()
    {
        if (On == false)
        {
            On = true;

            EffectScreen.SetActive(true);

            if (!Init)
            {
                for (int i = 0; i <= 7; i++)
                {
                    Boxes[i] = GameObject.Find("Box " + "(" + (i + 1).ToString() + ")").gameObject;
                    Boxes_Start_Position[i] = GameObject.Find("Box Start Position " + "(" + (i + 1).ToString() + ")").transform;
                    Boxes_End_Position[i] = GameObject.Find("Box End Position " + "(" + (i + 1).ToString() + ")").transform;
                }

                Init = true;
            }

            for (int i = 0; i <= 7; i++)
            {
                Boxes[i].gameObject.SetActive(true);
                Boxes_End_Position[i].gameObject.SetActive(true);
                Boxes_Start_Position[i].position = new Vector2(Boxes_Start_Position[i].position.x, Random.Range(25.0f, 100.0f));
                Boxes[i].transform.position = Boxes_Start_Position[i].position;
            }

            Invoke("StopEffect", 1.25f);
        }
    }

    public void StopEffect()
    {
        On = false;

        for (int i = 0; i <= 7; i++)
        {
            Boxes[i].gameObject.SetActive(false);
        }

        EffectScreen.SetActive(false);
    }
}

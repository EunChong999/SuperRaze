using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenEffect : MonoBehaviour
{
    public bool On;
    public bool Init;

    public float interval;
    public GameObject EffectScreen;

    public Sprite[] sprites = new Sprite[4];
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
                for (int i = 0; i <= 14; i++)
                {
                    Boxes[i] = GameObject.Find("Box " + "(" + (i + 1).ToString() + ")").gameObject;
                    Boxes_Start_Position[i] = GameObject.Find("Box Start Position " + "(" + (i + 1).ToString() + ")").transform;
                    Boxes_End_Position[i] = GameObject.Find("Box End Position " + "(" + (i + 1).ToString() + ")").transform;
                    Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
                }

                Init = true;
            }

            for (int i = 0; i <= 14; i++)
            {
                interval = Random.Range(0.035f, 0.35f);
                StartCoroutine(DropBalls(interval, i));
            }

            Invoke("StopEffect", 1.25f);
        }
    }

    private IEnumerator DropBalls(float interval, int i)
    {
        yield return new WaitForSeconds(interval);
        Boxes[i].gameObject.SetActive(true);
        Boxes_End_Position[i].gameObject.SetActive(true);
        Boxes_Start_Position[i].position = new Vector2(Boxes_Start_Position[i].position.x, Boxes_Start_Position[i].position.y);
        Boxes[i].transform.position = Boxes_Start_Position[i].position;
    }

    public void StopEffect()
    {
        On = false;

        for (int i = 0; i <= 14; i++)
        {
            Boxes[i].gameObject.SetActive(false);
        }

        EffectScreen.SetActive(false);
    }
}

using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class ScreenEffect : MonoBehaviour
{
    public bool On;
    public bool Init;

    public float interval;
    public ScreenChange ScreenChange;
    public GameObject EffectScreen;

    public UnityEngine.UI.Image blockImage; 
    public Sprite[] sprites = new Sprite[3];
    public GameObject[] backgrounds = new GameObject[3];
    public GameObject[] Boxes;
    public Transform[] Boxes_Start_Position;
    public Transform[] Boxes_End_Position;

    private void Update()
    {
        
    }

    public void AffectScreen()
    {
        if (On == false)
        {
            On = true;

            EffectScreen.SetActive(true);
            
            for (int i = 0; i <= 14; i++)
            {
                if (!Init) 
                {
                    Boxes[i] = GameObject.Find("Box " + "(" + (i + 1).ToString() + ")").gameObject;
                    Boxes_Start_Position[i] = GameObject.Find("Box Start Position " + "(" + (i + 1).ToString() + ")").transform;
                    Boxes_End_Position[i] = GameObject.Find("Box End Position " + "(" + (i + 1).ToString() + ")").transform;
                }

                switch (ScreenChange.CurrentScreenNumber)
                { 
                    case 0: // 인트로
                        Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
                        Boxes[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = backgrounds[0].GetComponent<SpriteRenderer>().color;
                        blockImage.color = backgrounds[0].GetComponent<SpriteRenderer>().color;
                        break;
                    case 1: // 메인
                        Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
                        Boxes[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = backgrounds[1].GetComponent<SpriteRenderer>().color;
                        blockImage.color = backgrounds[1].GetComponent<SpriteRenderer>().color;
                        break;
                    case 2: // 스테이지 1
                        Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
                        Boxes[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = backgrounds[1].GetComponent<SpriteRenderer>().color;
                        blockImage.color = backgrounds[1].GetComponent<SpriteRenderer>().color;
                        break;
                    case 3: // 스테이지 1 스코어
                        Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[2];
                        Boxes[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = backgrounds[2].GetComponent<SpriteRenderer>().color;
                        blockImage.color = backgrounds[2].GetComponent<SpriteRenderer>().color;
                        break;
                    case 4: // 스테이지 2
                        Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[2];
                        Boxes[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = backgrounds[2].GetComponent<SpriteRenderer>().color;
                        blockImage.color = backgrounds[2].GetComponent<SpriteRenderer>().color;
                        break;
                    case 5: // 스테이지 2 스코어
                        Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
                        Boxes[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = backgrounds[0].GetComponent<SpriteRenderer>().color;
                        blockImage.color = backgrounds[0].GetComponent<SpriteRenderer>().color;
                        break;
                    case 6: // 엔딩
                        Boxes[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
                        Boxes[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = backgrounds[0].GetComponent<SpriteRenderer>().color;
                        blockImage.color = backgrounds[0].GetComponent<SpriteRenderer>().color;
                        break;
                    default: 
                        break;
                }
            }

            Init = true;
            

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

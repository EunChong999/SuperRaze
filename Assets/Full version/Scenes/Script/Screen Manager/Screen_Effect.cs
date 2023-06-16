using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screen_Effect : MonoBehaviour
{
    public bool On;

    public int BoxCount;
    public GameObject Box;
    public Transform Box_Start_Position;
    public Transform Box_End_Position;

    public GameObject ScreenEffect;
    public GameObject[] Boxes;
    public Transform[] Boxes_Start_Position;
    public Transform[] Boxes_End_Position;

    private void Start()
    {
        for (int i = 0; i < BoxCount; i++)
        {
            // 각 박스에 박스 프리펩 할당
            Boxes[i] = Box;
            
            // 각 박스 시작 위치에 박스 시작 위치 할당, 가로로 4칸씩 떨어지도록 조정
            Vector3 Carrot_Start_Position_Temp = Boxes_Start_Position[i].position;
            Carrot_Start_Position_Temp.x = Box_Start_Position.position.x + i * 4;
            Carrot_Start_Position_Temp.y = Box_Start_Position.position.y;

            // 각 박스 종료 위치에 박스 종료 위치 할당, 가로로 4칸씩 떨어지도록 조정
            Vector3 Carrot_End_Position_Temp = Boxes_End_Position[i].position;
            Carrot_End_Position_Temp.x = Box_End_Position.position.x + i * 4;
            Carrot_End_Position_Temp.y = Box_End_Position.position.y;
        }

    }

    public void AffectScreen()
    {
        if(On == false)
        {
            On = true;

            ScreenEffect.SetActive(true);

            for (int i = 0; i < Boxes.Length; i++)
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

        for (int i = 0; i < Boxes.Length; i++)
        {
            Boxes[i].gameObject.SetActive(false);
        }

        ScreenEffect.SetActive(false);
    }
}

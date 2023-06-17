using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenBlock : MonoBehaviour
{
    public bool On;

    public float LerpTime;
    public float CurrentTime;

    public GameObject Block_Screen;

    public Transform Release_Start_Position;
    public Transform Release_End_Position;

    private void Update()
    {
        if(On == true)
        {
            CurrentTime += Time.deltaTime;

            if(CurrentTime >= LerpTime)
            {
                CurrentTime = LerpTime;
            }

            Block_Screen.transform.position = Vector3.Lerp(Release_Start_Position.position, Release_End_Position.position, CurrentTime / LerpTime);
        }
    }

    public void BlockScreen()
    {
        if (On == false)
        {
            Block_Screen.transform.position = Release_Start_Position.position;
            CurrentTime = 0;

            Invoke("CreateWall", 1.25f); 
        }
    }

    public void CreateWall()
    {
        On = true;

        Block_Screen.SetActive(true);

        Invoke("ReleaseWall", 0.5f);
    }

    public void ReleaseWall()
    {
        On = false;

        Block_Screen.SetActive(false);
    }
}

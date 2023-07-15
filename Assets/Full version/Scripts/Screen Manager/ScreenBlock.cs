using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenBlock : MonoBehaviour
{
    public bool on;

    public float LerpTime;
    public float CurrentTime;

    public GameObject Block_Screen;
    public GameObject[] Main_Screen = new GameObject[2];

    public Transform Release_Start_Position;
    public Transform Release_End_Position;

    private void Update()
    {
        if(on == true)
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
        if (on == false)
        {
            Block_Screen.transform.position = Release_Start_Position.position;
            CurrentTime = 0;

            Invoke("CreateWall", 1.25f); 
        }
    }

    public void CreateWall()
    {
        on = true;

        Block_Screen.SetActive(true);

        Invoke("ReleaseWall", 0.5f);
    }

    public void ReleaseWall()
    {
        on = false;

        Block_Screen.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public bool On;

    public int CurrentScreenNumber;

    public string CurrentScreenName;

    public Scene CurrentScene;

    private void Update()
    {
        CurrentScene = SceneManager.GetActiveScene();
        CurrentScreenNumber = CurrentScene.buildIndex;
        CurrentScreenName = CurrentScene.name;
    }

    public void ChangeScreen()
    {
        if(On == false)
        {
            On = true;

            Invoke("ScreenChanged", 1.25f);
        }
    }

    public void ScreenChanged()
    {
        if (CurrentScene.buildIndex + 1 < 5)
        {
            SceneManager.LoadScene(CurrentScreenNumber + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

        On = false;
    }
}

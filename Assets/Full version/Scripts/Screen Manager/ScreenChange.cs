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

    public void RestartScreen()
    {
        if (On == false)
        {
            On = true;

            Invoke("ScreenRestarted", 1.25f);
        }
    }

    public void ScreenRestarted()
    {
        SceneManager.LoadScene(CurrentScreenNumber);

        On = false;
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
        if (CurrentScene.buildIndex + 1 < 7)
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

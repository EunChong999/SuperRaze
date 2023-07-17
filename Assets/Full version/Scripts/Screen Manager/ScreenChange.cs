using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public bool on;

    public int CurrentScreenNumber;

    public string CurrentScreenName;

    public Scene CurrentScene;

    public List<GameObject> ScreenUI;

    private void Update()
    {
        CurrentScene = SceneManager.GetActiveScene();
        CurrentScreenNumber = CurrentScene.buildIndex;
        CurrentScreenName = CurrentScene.name;

        ManageScreenUI();
    }

    private void ManageScreenUI()
    {
        for(int i = 0; i < ScreenUI.Count; i++)
        {
            if (i == CurrentScreenNumber) 
            {
                ScreenUI[i].SetActive(true);
            }
            else
            {
                ScreenUI[i].SetActive(false);
            }
        }
    }

    public void RestartScreen()
    {
        if (on == false)
        {
            on = true;

            Invoke("ScreenRestarted", 1.25f);
        }
    }

    public void ScreenRestarted()
    {
        SceneManager.LoadScene(CurrentScreenNumber);

        on = false;
    }

    public void ChangeScreen()
    {
        if(on == false)
        {
            on = true;

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

        on = false;
    }

    public void BackScreen()
    {
        if (on == false)
        {
            on = true;

            Invoke("ScreenBacked", 1.25f);
        }
    }

    public void ScreenBacked()
    {
        if (CurrentScene.buildIndex + 1 < 7)
        {
            SceneManager.LoadScene(CurrentScreenNumber - 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

        on = false;
    }

    public void GiveUp()
    {
        if (on == false)
        {
            on = true;

            Invoke("GivedUp", 1.25f);
        }
    }

    public void GivedUp()
    {
        SceneManager.LoadScene(1);

        on = false;
    }
}

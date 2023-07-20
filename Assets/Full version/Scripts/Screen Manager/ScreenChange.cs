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

    [SerializeField] private GameObject scoreManager;

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

            if (CurrentScreenNumber == 2)
            {
                scoreManager.GetComponent<Score>().ResetScore1();
            }
            else if (CurrentScreenNumber == 4)
            {
                scoreManager.GetComponent<Score>().ResetScore2();
            }

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
            SceneManager.LoadScene(1);
        }

        on = false;
    }

    public void GiveUp()
    {
        if (on == false)
        {
            on = true;

            scoreManager.GetComponent<Score>().ResetScore1();
            scoreManager.GetComponent<Score>().ResetScore2();

            Invoke("GivedUp", 1.25f);
        }
    }

    public void GivedUp()
    {
        SceneManager.LoadScene(1);
        on = false;
    }
}

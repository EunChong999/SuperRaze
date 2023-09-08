using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

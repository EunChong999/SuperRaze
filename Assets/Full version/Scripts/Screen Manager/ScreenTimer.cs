using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTimer : MonoBehaviour
{
    public bool isTimeStop;

    public void MoveTime()
    {
        isTimeStop = false;
        Time.timeScale = 1;
    }

    public void StopTime()
    {
        isTimeStop = true;
        Time.timeScale = 1;
    }
}

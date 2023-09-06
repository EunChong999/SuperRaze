using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTimer : MonoBehaviour
{
    public bool isTimeStop;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

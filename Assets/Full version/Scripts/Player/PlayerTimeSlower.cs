using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeSlower : MonoBehaviour
{
    [SerializeField] private float slowdownFactor = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartSlowmotion();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopSlowmotion();
        }
    }

    private void StartSlowmotion()
    {
        Time.timeScale = slowdownFactor;
    }

    private void StopSlowmotion()
    {
        Time.timeScale = 1;
    }
}

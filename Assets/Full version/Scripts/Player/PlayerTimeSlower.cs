using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeSlower : MonoBehaviour
{
    public float effectDelay;
    [SerializeField] private float slowdownFactor = 0.5f;
    public AfterImage afterImage;

    private void Start()
    {

    }

    private void Update()
    {
        afterImage.delay = effectDelay;

        if (Input.GetMouseButtonDown(1))
        {
            StartSlowmotion();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopSlowmotion();
        }
    }

    void StopAfterImage()
    {
        afterImage.makeImage = false;
    }

    private void StartSlowmotion()
    {
        CancelInvoke();
        afterImage.makeImage = true;
        Time.timeScale = slowdownFactor;
    }

    private void StopSlowmotion()
    {
        Invoke("StopAfterImage", 0.5f);
        Time.timeScale = 1;
    }
}

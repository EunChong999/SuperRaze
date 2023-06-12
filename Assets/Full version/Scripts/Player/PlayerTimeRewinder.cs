using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeRewinder : MonoBehaviour
{
    bool isRewinding = false;

    public float recordTime = 5f;

    List<PlayerPointTime> playerPointTime;
   
    public Rigidbody2D rb;
    public GameObject body;

    // Use this for initialization
    void Start()
    {
        playerPointTime = new List<PlayerPointTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }

        if (Input.GetMouseButtonDown(1))
            StartRewind();
        if (Input.GetMouseButtonUp(1))
            StopRewind();
    }

    void Rewind()
    {
        if (playerPointTime.Count > 0)
        {
            PlayerPointTime pointInTime = playerPointTime[0];
            body.transform.position = pointInTime.position;
            body.transform.rotation = pointInTime.rotation;
            playerPointTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if (playerPointTime.Count > Mathf.Round(recordTime / Time.deltaTime))
        {
            playerPointTime.RemoveAt(playerPointTime.Count - 1);
        }

        playerPointTime.Insert(0, new PlayerPointTime(body.transform.position, body.transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTimeRewinder : MonoBehaviour
{
    bool isRewinding = false;

    public float effectDelay;
    public float recordTime = 5f;
    public float rewindSpeed = 5f;

    Stack<PlayerPointTime> playerPointTime;

    public Rigidbody2D rb;
    public GameObject body;
    public AfterImage afterImage;

    private Vector3 targetPosition;
    private Vector3 previousPosition;
    private float lerpStartTime;
    private float lerpDuration;
    private float curTime = 0;

    // Use this for initialization
    void Start()
    {
        playerPointTime = new Stack<PlayerPointTime>();
    }

    // Update is called once per frame
    void Update()
    {
        afterImage.delay = effectDelay;

        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartRewind();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopRewind();
        }
    }

    void Rewind()
    {
        if (playerPointTime.Count > 0)
        {
            curTime += Time.deltaTime;

            if (recordTime - curTime <= 0)
            {
                StopRewind();
            }
            else
            {
                float timeSinceLerpStart = Time.time - lerpStartTime;
                float t = Mathf.Clamp01(timeSinceLerpStart / (lerpDuration / rewindSpeed)); // rewindSpeed로 나누어 보정
                body.transform.position = Vector3.Lerp(previousPosition, targetPosition, t);

                if (t >= 1f)
                {
                    PlayerPointTime pointInTime = playerPointTime.Pop();
                    targetPosition = pointInTime.position;
                    body.transform.rotation = pointInTime.rotation;
                    previousPosition = body.transform.position;
                    lerpStartTime = Time.time;
                    lerpDuration = Vector3.Distance(previousPosition, targetPosition) / rewindSpeed;
                }
            }
        }
        else
        {
            StopRewind();
        }
    }


    void Record()
    {
        if (rb.velocity.x != 0 && rb.velocity.y != 0)
        {
            playerPointTime.Push(new PlayerPointTime(body.transform.position, body.transform.rotation));
        }
    }


    public void StartRewind()
    {
        afterImage.makeImage = true;
        isRewinding = true;
        rb.isKinematic = true;

        if (playerPointTime.Count > 0)
        {
            PlayerPointTime pointInTime = playerPointTime.Peek();
            targetPosition = pointInTime.position;
            body.transform.rotation = pointInTime.rotation;
            previousPosition = body.transform.position;
            lerpStartTime = Time.time;
            lerpDuration = Vector3.Distance(previousPosition, targetPosition) / rewindSpeed;
        }
    }

    public void StopRewind()
    {
        afterImage.makeImage = false;
        curTime = 0;
        playerPointTime.Clear();
        isRewinding = false;
        rb.isKinematic = false;
    }
}

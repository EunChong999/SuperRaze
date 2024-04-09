using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTimeRewinder : MonoBehaviour
{
    public float effectDelay;
    [HideInInspector] public bool isRewinding = false;
    public float rewindSpeed = 5f;

    [SerializeField] private PlayerHealther playerHealther;
    [SerializeField] private SecondPlayerController secondPlayerController;

    Stack<PlayerPointTime> playerPointTime;

    public Rigidbody2D rb;
    public GameObject body;
    public AfterImage afterImage;

    private Vector3 targetPosition;
    private Vector3 previousPosition;
    private float lerpStartTime;
    private float lerpDuration;
    [SerializeField] private GameObject skillEffect;
    ScreenTimer screenTimer;

    // Use this for initialization
    void Start()
    {
        playerPointTime = new Stack<PlayerPointTime>();
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
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

        if (Input.GetMouseButton(1))
        {
            if (playerPointTime.Count != 0)
            {
                playerHealther.UseSkill();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (playerHealther.currentEnergy >= 0 && !screenTimer.isTimeStop)
            {
                StartRewind();
            }
        }

        if (Input.GetMouseButtonUp(1) || playerPointTime.Count == 0 || playerHealther.currentEnergy <= 0 || screenTimer.isTimeStop)
        {
            StopRewind();
        }
    }

    void Rewind()
    {
        if (playerPointTime.Count > 0)
        {
            float timeSinceLerpStart = Time.time - lerpStartTime;
            float t = Mathf.Clamp01(timeSinceLerpStart / (lerpDuration / rewindSpeed)); // rewindSpeed
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
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            playerPointTime.Push(new PlayerPointTime(body.transform.position, body.transform.rotation));
        }
    }

    public void StartRewind()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(235f / 255f, 75f / 255f, 76f / 255f, 0.4f);
        afterImage.makeImage = true;
        skillEffect.SetActive(true);
        isRewinding = true;

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
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        afterImage.makeImage = false;
        playerPointTime.Clear();
        skillEffect.SetActive(false);
        isRewinding = false;
    }
}

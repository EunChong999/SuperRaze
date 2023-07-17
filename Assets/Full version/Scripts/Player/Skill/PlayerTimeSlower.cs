using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeSlower : MonoBehaviour
{
    public float effectDelay;
    [SerializeField] private float slowdownFactor = 0.5f;
    public AfterImage afterImage;
    [HideInInspector] public bool isSlowering;
    [SerializeField] private FirstPlayerCollisionChecker firstPlayerCollision;
    [SerializeField] private PlayerHealther playerHealther;

    private void OnEnable()
    {
        StopSlowmotion();
    }

    private void Start()
    {

    }

    private void Update()
    {
        afterImage.delay = effectDelay;

        if (Input.GetMouseButton(1))
        {
            playerHealther.UseSkill();

            if (playerHealther.currentEnergy > 10)
            {
                StartSlowmotion();
            }
            else
            {
                StopSlowmotion();
            }
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
        isSlowering = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        CancelInvoke();
        afterImage.makeImage = true;
        Time.timeScale = slowdownFactor;
    }

    private void StopSlowmotion()
    {
        if (isSlowering && firstPlayerCollision.collisionObject != null)
        {
            firstPlayerCollision.StartDamage();
        }

        isSlowering = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Invoke("StopAfterImage", 0.5f);
        Time.timeScale = 1;
    }
}

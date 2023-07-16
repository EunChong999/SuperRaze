using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemyHealther : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    Material material;
    [HideInInspector] public bool isDissolving;
    float fade;
    private GameObject screenManager;
    [SerializeField] private TrackingEnemyCollisionChecker trackingEnemyCollisionChecker;

    void Start()
    {
        screenManager = GameObject.Find("Screen Manager");

        material = transform.GetChild(0).GetComponent<SpriteRenderer>().material;

        isDead = false;

        fade = 0;
        isDissolving = true;
    }

    void Update()
    {
        // ü�� �� ����
        if (!screenManager.GetComponent<ScreenTimer>().isTimeStop)
        {
            if (trackingEnemyCollisionChecker.isDamage)
            {
                isDead = true;
                isDissolving = true;
            }

            if (!isDead)
            {
                Spawn();
            }
            else
            {
                Dead();
            }
        }
    }

    // �÷��̾� ����
    public void Spawn()
    {
        if (isDissolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1)
            {
                fade = 1;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }

    // �÷��̾� ���
    public void Dead()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0)
            {
                fade = 0;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}
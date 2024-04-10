using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealther : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    Material material;
    [HideInInspector] public bool isDissolving;
    float fade;
    [HideInInspector] public ScreenTimer screenTimer;
    [SerializeField] private CollisionChecker collisionChecker;
    [SerializeField] private GameObject collisionCheck;
    protected Timer timer;

    private void Start()
    {

    }

    virtual public void Init()
    {
        collisionCheck.SetActive(false);

        material = transform.GetChild(0).GetComponent<SpriteRenderer>().material;

        isDead = false;

        fade = 0;
        isDissolving = true;
    }

    virtual public void Live()
    {
        if (timer == null)
            timer = FindObjectOfType<Timer>();

        // ü�� �� ����
        if (!screenTimer.isTimeStop)
        {
            if (timer.isTimeOver && !isDead)
            {
                isDead = true;
                isDissolving = true;
            }
            else if (collisionChecker.isDamage)
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
    protected void Spawn()
    {
        if (isDissolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1)
            {
                fade = 1;
                isDissolving = false;
                collisionCheck.SetActive(true);
            }

            material.SetFloat("_Fade", fade);
        }
    }

    // �÷��̾� ���
    protected void Dead()
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

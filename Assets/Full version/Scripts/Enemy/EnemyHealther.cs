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

    virtual public void Init()
    {
        screenTimer = GameObject.FindWithTag("ScreenTimer").GetComponent<ScreenTimer>();
        collisionCheck.SetActive(false);

        material = transform.GetChild(0).GetComponent<SpriteRenderer>().material;

        isDead = false;

        fade = 0;
        isDissolving = true;
    }

    virtual public void Live()
    {
        // 체력 및 마나
        if (!screenTimer.isTimeStop)
        {
            if (collisionChecker.isDamage)
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

    // 플레이어 생성
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

    // 플레이어 사망
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

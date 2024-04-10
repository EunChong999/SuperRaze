using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingEnemyHealther : EnemyHealther
{
    [SerializeField] private BombingEnemyExploder bombingEnemyExploder;

    public override void Init()
    {
        base.Init();
        bombingEnemyExploder = gameObject.GetComponent<BombingEnemyExploder>();
    }

    public override void Live()
    {
        if (timer == null)
            timer = FindObjectOfType<Timer>();

        // 체력 및 마나
        if (!screenTimer.isTimeStop)
        {
            if (timer.isTimeOver && !isDead)
            {
                isDead = true;
                isDissolving = true;
            }
            else if (bombingEnemyExploder.isBombing)
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

    void Start()
    {
        Init();
    }

    void Update()
    {
        Live();
    }
}

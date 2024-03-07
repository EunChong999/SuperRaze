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
        // ü�� �� ����
        if (!screenTimer.isTimeStop)
        {
            if (bombingEnemyExploder.isBombing)
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

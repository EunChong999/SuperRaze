using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyHealther : EnemyHealther
{
    void Start()
    {
        Init();
    }

    void Update()
    {
        Live();
    }
}

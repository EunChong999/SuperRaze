using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyHealther : EnemyHealther
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

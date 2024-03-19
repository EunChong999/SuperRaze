using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingEnemyCollisionChecker : CollisionChecker
{
    [SerializeField] BombingEnemyExploder bombingEnemyExploder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Collision();
            bombingEnemyExploder.Bombed();
            bombingEnemyExploder.isBombing = true;
        }
    }
}

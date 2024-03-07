using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingEnemyCollisionChecker : CollisionChecker
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Collision();
            gameObject.transform.parent.parent.GetComponent<BombingEnemyExploder>().Bombed();
            gameObject.transform.parent.parent.GetComponent<BombingEnemyExploder>().isBombing = true;
        }
    }
}

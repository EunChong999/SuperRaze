using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WalkingEnemyPatroller : BasicEnemy
{
    protected override void Init()
    {
        base.Init();
        anim.SetBool("isRun", true);
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (currentPoint == pointB)
        {
            rb.velocity = new Vector2(movingSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-movingSpeed, 0);
        }

        if (Vector2.Distance(body.position, currentPoint.position) < 1f && currentPoint == pointB)
        {
            Flip();
            currentPoint = pointA;
        }

        if (Vector2.Distance(body.position, currentPoint.position) < 1f && currentPoint == pointA)
        {
            Flip();
            currentPoint = pointB;
        }
    }

    private void Flip()
    {
        Vector3 localScale = body.localScale;
        localScale.x *= -1;
        body.localScale = localScale;  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);  
    }
}

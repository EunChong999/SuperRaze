using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutingEnemyPatroller : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform body;
    [SerializeField] private Animator anim;
    [SerializeField] private float movingSpeed;
    private Transform currentPoint;

    private void Start()
    {
        pointA.transform.parent = transform;
        pointB.transform.parent = transform;
        currentPoint = pointB.transform;
        anim.SetBool("isRun", true);
    }

    private void Update()
    {
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(movingSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-movingSpeed, 0);
        }

        if (Vector2.Distance(body.position, currentPoint.position) < 1f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(body.position, currentPoint.position) < 1f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
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

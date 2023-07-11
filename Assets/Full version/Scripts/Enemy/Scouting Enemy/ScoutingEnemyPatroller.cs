using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ScoutingEnemyPatroller : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    private Vector3 pointAtf;
    [SerializeField] private Transform pointB;
    private Vector3 pointBtf;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform body;
    [SerializeField] private Animator anim;
    [SerializeField] private float movingSpeed;
    private Transform currentPoint;
    private Vector3 currentTransform;

    private void Start()
    {
        // �ӵ� ���� ����
        movingSpeed = Random.Range(5.0f, 10.0f);

        // ���� ���� �� �������� ������ �θ�κ��� �ع�
        pointA.transform.parent = transform;
        pointB.transform.parent = transform;
        currentPoint = pointB;

        // �������� ������ �Ҵ�
        pointAtf = pointA.position;
        pointBtf = pointB.position;
        pointAtf.x = transform.parent.GetChild(0).position.x;
        pointBtf.x = transform.parent.GetChild(1).position.x;
        pointA.position = pointAtf;
        pointB.position = pointBtf;

        // ���� ���� ������ ���� ��ġ ����
        currentTransform = transform.GetChild(0).transform.position;
        currentTransform.x = Random.Range(pointA.position.x, pointB.position.x);
        transform.GetChild(0).transform.position = currentTransform;

        anim.SetBool("isRun", true);
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

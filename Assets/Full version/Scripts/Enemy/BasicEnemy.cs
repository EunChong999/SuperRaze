using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] protected Transform pointA;
    protected Vector3 pointAtf;
    [SerializeField] protected Transform pointB;
    protected Vector3 pointBtf;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform body;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float movingSpeed;
    protected Transform currentPoint;
    protected Vector3 currentTransform;

    protected virtual void Init()
    {
        // 속도 랜덤 지정
        movingSpeed = Random.Range(minSpeed, maxSpeed);

        // 방향 지정 및 시작점과 끝점을 부모로부터 해방
        pointA.transform.parent = transform;
        pointB.transform.parent = transform;
        currentPoint = pointB;

        // 시작점과 끝점을 할당
        pointAtf = pointA.position;
        pointBtf = pointB.position;
        pointAtf.x = transform.parent.GetChild(0).position.x;
        pointBtf.x = transform.parent.GetChild(1).position.x;
        pointA.position = pointAtf;
        pointB.position = pointBtf;

        // 일정 범위 사이의 랜덤 위치 지정
        currentTransform = transform.GetChild(0).transform.position;
        currentTransform.x = Random.Range(pointA.position.x, pointB.position.x);
        transform.GetChild(0).transform.position = currentTransform;
    }
}

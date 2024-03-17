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
        // �ӵ� ���� ����
        movingSpeed = Random.Range(minSpeed, maxSpeed);

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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingEnemyExploder : MonoBehaviour
{
    public GameObject body;
    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField] private Transform pointA;
    private Vector3 pointAtf;
    [SerializeField] private Transform pointB;
    private Vector3 pointBtf;
    private Vector3 currentTransform;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float chasingSpeed;
    private float chasingSpeedTemp;
    public int patrolDestination;

    private Transform playerTransform;
    public bool isChasing;
    public bool isGrounding;
    public bool isUnGrounded;
    public float chaseDistance;
    [SerializeField] private GameObject ExplosionObj;
    private bool isDead;
    [HideInInspector]public bool isBombing;
    [SerializeField] private GameObject collisionChecker;

    private void Start()
    {
        // 속도 랜덤 지정
        movingSpeed = Random.Range(2.5f, 5.0f);
        chasingSpeed = Random.Range(5.0f, 7.5f);

        // 플레이어 오브젝트 찾기 및 시작점과 끝점을 부모로부터 해방
        playerTransform = GameObject.Find("Player").transform.GetChild(0);
        pointA.parent = transform;
        pointB.parent = transform;

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

        // 상태 초기화
        isChasing = false;
        animator.SetBool("isRun", true);
        chasingSpeedTemp = chasingSpeed;
        isDead = false;
        isBombing = false;

        // 폭발 비활성화
        ExplosionObj.SetActive(false);
    }

    private void Update()
    {
        if (!isBombing)
        {
            CheckGround();
            CheckSpeed();
            CheckCollision();
            DecideChasing();
            Move();
        }
    }

    void CheckGround()
    {
        float x = body.transform.localScale.x;
        Vector2 frontVec = new Vector2(rb.position.x + x / 10, rb.position.y - 0.5f);
        Debug.DrawRay(frontVec, Vector3.down, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Object"));
        if (rayHit.collider == null)
        {
            isGrounding = false;
        }
        else
        {
            isGrounding = true;
        }
    }

    void CheckSpeed()
    {
        if (chasingSpeed == 0)
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
        }
    }

    void CheckCollision()
    {
        if (Mathf.Abs(body.transform.position.x - playerTransform.position.x) < 3 && isChasing)
        {
            chasingSpeed = 0;

            if (!isBombing)
            {
                Invoke("Bomb", 0.4f);
                isBombing = true;
            }
        }
        else
        {
            chasingSpeed = chasingSpeedTemp;
        }
    }

    public void Bomb()
    {
        if(!isDead)
        {
            collisionChecker.SetActive(false);
            ExplosionObj.SetActive(true);
            Invoke("Bombed", 0.8f);
            isDead = true;
        }
    }

    public void Bombed()
    {
        ExplosionObj.SetActive(false);
    }

    void DecideChasing()
    {
        if (Mathf.Abs(body.transform.position.x - playerTransform.position.x) < chaseDistance &&
            Mathf.Abs(body.transform.position.y - playerTransform.position.y) < 1 &&
            isGrounding &&
            playerTransform.transform.position.x > pointA.position.x &&
            playerTransform.transform.position.x < pointB.position.x &&
            playerTransform.gameObject.layer == 7)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }

    void Move()
    {
        if (!isGrounding)
        {
            if (body.transform.localScale == new Vector3(1, 1, 1))
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
                Vector3 temp = body.transform.position;
                temp.x -= 1;
                body.transform.position = temp;
            }
            else if (body.transform.localScale == new Vector3(-1, 1, 1))
            {
                body.transform.localScale = new Vector3(1, 1, 1);
                Vector3 temp = body.transform.position;
                temp.x += 1;
                body.transform.position = temp;
            }
        }
        else
        {
            if (isChasing)
            {
                if (body.transform.position.x > playerTransform.position.x)
                {
                    body.transform.localScale = new Vector3(-1, 1, 1);
                    body.transform.position += Vector3.left * chasingSpeed * Time.deltaTime;
                }
                if (body.transform.position.x < playerTransform.position.x)
                {
                    body.transform.localScale = new Vector3(1, 1, 1);
                    body.transform.position += Vector3.right * chasingSpeed * Time.deltaTime;
                }
            }
            else
            {
                if (patrolDestination == 0 && body.transform.localScale == new Vector3(1, 1, 1))
                {
                    body.transform.localScale = new Vector3(-1, 1, 1);
                }
                if (patrolDestination == 1 && body.transform.localScale == new Vector3(-1, 1, 1))
                {
                    body.transform.localScale = new Vector3(1, 1, 1);
                }

                if (patrolDestination == 0)
                {
                    body.transform.position = Vector2.MoveTowards(body.transform.position, pointA.position, movingSpeed * Time.deltaTime);
                    if (Vector2.Distance(body.transform.position, pointA.position) < .2f)
                    {
                        body.transform.localScale = new Vector3(1, 1, 1);
                        patrolDestination = 1;
                    }
                }
                if (patrolDestination == 1)
                {
                    body.transform.position = Vector2.MoveTowards(body.transform.position, pointB.position, movingSpeed * Time.deltaTime);
                    if (Vector2.Distance(body.transform.position, pointB.position) < .2f)
                    {
                        body.transform.localScale = new Vector3(-1, 1, 1);
                        patrolDestination = 0;
                    }
                }
            }
        }
    }
}

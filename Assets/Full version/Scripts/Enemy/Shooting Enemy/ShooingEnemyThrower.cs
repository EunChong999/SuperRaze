using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooingEnemyThrower : MonoBehaviour
{
    // Shoot
    [SerializeField] private float coolTime;
    [SerializeField] private static ShooingEnemyThrower instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private int amountToPool;

    [SerializeField] private GameObject bulletPrefeb;
    [SerializeField] private Transform bulletPosition;

    private void Awake()
    {
        // Shoot
        if (instance == null)
        {
            instance = this;
        }
    }

    // GetPooledObject & Shoot
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    IEnumerator Shoot()
    {
        animator.SetBool("isRun", false);

        GameObject Bullet = GetPooledObject();

        if (Bullet != null)
        {
            Bullet.transform.position = bulletPosition.position;
            Bullet.SetActive(true);
        }

        isShooting = true;

        yield return new WaitForSeconds(coolTime);

        isShooting = false;
    }

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
    public bool isShooting;
    public bool isGrounding;
    public bool isUnGrounded;
    public float chaseDistance;

    private void Start()
    {
        // 사격
        isShooting = false;

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject Obj = Instantiate(bulletPrefeb);
            Obj.SetActive(false);
            pooledObjects.Add(Obj);
        }

        // 속도 랜덤 지정
        movingSpeed = Random.Range(2.5f, 5.0f);
        chasingSpeed = Random.Range(5.0f, 7.5f);

        // 플레이어 오브젝트 찾기 및 시작점과 끝점을 부모로부터 해방
        playerTransform = GameObject.Find("Player").transform.GetChild(0);
        pointA.parent = transform;
        pointB.parent = transform;

        // 시작점과 끝점을 할당
        //pointAtf = pointA.position;
        //pointBtf = pointB.position;
        //pointAtf.x = transform.parent.GetChild(0).position.x;
        //pointBtf.x = transform.parent.GetChild(1).position.x;
        //pointA.position = pointAtf;
        //pointB.position = pointBtf;

        // 일정 범위 사이의 랜덤 위치 지정
        currentTransform = transform.GetChild(0).transform.position;
        currentTransform.x = Random.Range(pointA.position.x, pointB.position.x);
        transform.GetChild(0).transform.position = currentTransform;

        isShooting = false;
        animator.SetBool("isRun", true);
        chasingSpeedTemp = chasingSpeed;
    }

    private void Update()
    {
        CheckGround();
        CheckSpeed();
        CheckCollision();
        DecideShooting();
        Move();
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
        if (Mathf.Abs(body.transform.position.x - playerTransform.position.x) < 0.5f)
        {
            chasingSpeed = 0;
        }
        else
        {
            chasingSpeed = chasingSpeedTemp;
        }
    }

    void DecideShooting()
    {
        if (Mathf.Abs(body.transform.position.x - playerTransform.position.x) < chaseDistance &&
            ((playerTransform.position.y + 6) - body.transform.position.y) > 0 &&
            Mathf.Abs(body.transform.position.y - playerTransform.position.y) < chaseDistance / 2 &&
            isGrounding &&
            playerTransform.transform.position.x > pointA.position.x &&
            playerTransform.transform.position.x < pointB.position.x &&
            playerTransform.gameObject.layer == 7)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
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
            if (isShooting)
            {
                StartCoroutine(Shoot());
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


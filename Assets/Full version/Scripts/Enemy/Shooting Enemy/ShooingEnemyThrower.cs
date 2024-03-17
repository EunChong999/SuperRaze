using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooingEnemyThrower : BasicEnemy
{
    // Shoot
    public bool isShooting;
    public bool isShooted;
    public bool canShoot;
    [SerializeField] private float coolTime;
    [SerializeField] private static ShooingEnemyThrower instance;
    public Animator animator;

    private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private int amountToPool;

    [SerializeField] private GameObject bulletPrefeb;
    [SerializeField] private Transform bulletPosition;

    public int patrolDestination;

    private Transform playerTransform;

    public bool isGrounding;
    public bool isUnGrounded;
    public float chaseDistance;

    [SerializeField] AudioSource shoot;

    private void Awake()
    {
        InitPooling();
    }

    private void InitPooling()
    {
        // Shoot
        if (instance == null)
        {
            instance = this;
        }

        // 슈팅 풀링
        isShooting = false;
        isShooted = false;

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject Obj = Instantiate(bulletPrefeb, transform.GetChild(0));
            Obj.SetActive(false);
            pooledObjects.Add(Obj);
        }
    }

    protected override void Init()
    {
        base.Init();

        // 상태 초기화
        shoot = GameObject.Find("EnemyShoot").GetComponent<AudioSource>();
        playerTransform = GameObject.Find("Player").transform.GetChild(0);
        canShoot = false;
        animator.SetBool("isRun", true);
    }

    private void Start()
    {
        Init();
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
        shoot.Play();

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

        isShooted = false;
    }

    private void Update()
    {
        CheckGround();
        CheckSpeed();
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
        if (canShoot)
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
        }
    }

    void DecideShooting()
    {
        if (Mathf.Abs(body.transform.position.x - playerTransform.position.x) < chaseDistance &&
            Mathf.Abs(body.transform.position.y - playerTransform.position.y) < 1 &&
            isGrounding &&
            playerTransform.transform.position.x > pointA.position.x &&
            playerTransform.transform.position.x < pointB.position.x &&
            playerTransform.gameObject.layer == 7)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
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
            if (canShoot)
            {
                if (body.transform.position.x > playerTransform.position.x)
                {
                    body.transform.localScale = new Vector3(-1, 1, 1);
                }
                if (body.transform.position.x < playerTransform.position.x)
                {
                    body.transform.localScale = new Vector3(1, 1, 1);
                }

                if (!isShooting && !isShooted)
                {

                    StartCoroutine(Shoot());
                    isShooted = true;
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


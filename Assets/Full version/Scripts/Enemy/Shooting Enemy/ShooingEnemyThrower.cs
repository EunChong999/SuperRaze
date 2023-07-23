using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooingEnemyThrower : MonoBehaviour
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

    public GameObject body;
    public Rigidbody2D rb;

    [SerializeField] private Transform pointA;
    private Vector3 pointAtf;
    [SerializeField] private Transform pointB;
    private Vector3 pointBtf;
    private Vector3 currentTransform;
    [SerializeField] private float movingSpeed;
    public int patrolDestination;

    private Transform playerTransform;

    public bool isGrounding;
    public bool isUnGrounded;
    public float chaseDistance;

    [SerializeField] AudioSource shoot;

    private void Awake()
    {
        // Shoot
        if (instance == null)
        {
            instance = this;
        }

        // ���� Ǯ��
        isShooting = false;
        isShooted = false;

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject Obj = Instantiate(bulletPrefeb, transform.GetChild(0));
            Obj.SetActive(false);
            pooledObjects.Add(Obj);
        }
    }

    private void Start()
    {
        shoot = GameObject.Find("EnemyShoot").GetComponent<AudioSource>();

        // �ӵ� ���� ����
        movingSpeed = Random.Range(2.5f, 5.0f);

        // �÷��̾� ������Ʈ ã�� �� �������� ������ �θ�κ��� �ع�
        playerTransform = GameObject.Find("Player").transform.GetChild(0);
        pointA.parent = transform;
        pointB.parent = transform;

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

        // ���� �ʱ�ȭ
        canShoot = false;
        animator.SetBool("isRun", true);
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


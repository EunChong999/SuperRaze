using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingEnemyExploder : BasicEnemy
{
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
    private CameraShake cameraShake;
    [SerializeField] AudioSource explod;

    protected override void Init()
    {
        explod = GameObject.Find("EnemyExplosion").GetComponent<AudioSource>();
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();

        base.Init();

        // 상태 초기화
        chasingSpeed = Random.Range(5.0f, 7.5f);
        playerTransform = GameObject.Find("Player").transform.GetChild(0);
        isChasing = false;
        anim.SetBool("isRun", true);
        chasingSpeedTemp = chasingSpeed;
        isDead = false;
        isBombing = false;
        ExplosionObj.SetActive(false);
        chasingSpeedTemp = chasingSpeed;
    }

    private void Start()
    {
        Init();
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
            anim.SetBool("isRun", false);
        }
        else
        {
            anim.SetBool("isRun", true);
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
            explod.Play();
            cameraShake.ShakeCamera(8, 0.5f);
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

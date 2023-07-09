using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackingEnemyChaser : MonoBehaviour
{
    public GameObject body;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform[] patrolPoints;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float chasingSpeed;
    private float chasingSpeedTemp;
    public int patrolDestination;

    private Transform playerTransform;
    public bool isChasing;
    public bool isGrounding;
    public bool isUnGrounded;
    public float chaseDistance;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform.GetChild(0);
        patrolPoints[0].parent = transform;
        patrolPoints[1].parent = transform;
        isChasing = false;
        animator.SetBool("isRun", true);
        chasingSpeedTemp = chasingSpeed;
    }

    private void Update()
    {
        CheckGround();
        CheckSpeed();
        CheckCollision();
        DecideChasing();
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

    void DecideChasing()
    {
        if (Mathf.Abs(body.transform.position.x - playerTransform.position.x) < chaseDistance &&
            ((playerTransform.position.y + 6) - body.transform.position.y) > 0 &&
            Mathf.Abs(body.transform.position.y - playerTransform.position.y) < chaseDistance / 2 &&
            isGrounding &&
            playerTransform.transform.position.x > patrolPoints[0].position.x &&
            playerTransform.transform.position.x < patrolPoints[1].position.x 
/*            playerTransform.gameObject.layer == 7*/)
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
                    body.transform.position = Vector2.MoveTowards(body.transform.position, patrolPoints[0].position, movingSpeed * Time.deltaTime);
                    if (Vector2.Distance(body.transform.position, patrolPoints[0].position) < .2f)
                    {
                        body.transform.localScale = new Vector3(1, 1, 1);
                        patrolDestination = 1;
                    }
                }
                if (patrolDestination == 1)
                {
                    body.transform.position = Vector2.MoveTowards(body.transform.position, patrolPoints[1].position, movingSpeed * Time.deltaTime);
                    if (Vector2.Distance(body.transform.position, patrolPoints[1].position) < .2f)
                    {
                        body.transform.localScale = new Vector3(-1, 1, 1);
                        patrolDestination = 0;
                    }
                }
            }
        }
    }
}


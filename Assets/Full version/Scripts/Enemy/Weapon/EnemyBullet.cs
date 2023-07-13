using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 24f;
    private GameObject enemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    private bool isDestroyed;

    private void Start()
    {
        enemy = transform.parent.gameObject;
        transform.parent = null;
        isDestroyed = false;
    }

    private void Update()
    {
        if (enemy.transform.localScale.x == 1 && rb.velocity.x == 0)
        {
            rb.velocity = transform.right * speed;
            sr.flipX = false;
        }
        else if (enemy.transform.localScale.x != 1 && rb.velocity.x == 0)
        {
            rb.velocity = transform.right * -1 * speed;
            sr.flipX = true;
        }

        if (!isDestroyed)
        {
            Invoke("BulletDestroy", 0.5f);
            isDestroyed = true;
        }
    }

    void BulletDestroy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") == true)
        {
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player") == true)
        {
            gameObject.SetActive(false);
        }
    }
}

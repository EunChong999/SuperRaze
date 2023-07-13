using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 24f;
    private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    private bool isDestroyed;

    private void Start()
    {
        player = GameObject.Find("Player");
        isDestroyed = false;
    }

    private void Update()
    {
        if (player.GetComponent<PlayerController>().isFacingRight  && rb.velocity.x == 0)
        {
            rb.velocity = transform.right * speed;
            sr.flipX = false;
        }
        else if (!player.GetComponent<PlayerController>().isFacingRight && rb.velocity.x == 0)
        {
            rb.velocity = transform.right * -1 * speed;
            sr.flipX = true;
        }

        if (!isDestroyed)
        {
            Invoke("BulletDestroy", 0.333f);
            isDestroyed = true;
        }
    }

    void BulletDestroy()
    {
        gameObject.SetActive(false);
        isDestroyed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") == true)
        {
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Enemy") == true)
        {
            gameObject.SetActive(false);
        }
    }
}

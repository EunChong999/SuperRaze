using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 24f;
    private GameObject player;
    private Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    private void Start()
    {
        player = GameObject.Find("Player");
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

        Invoke("BulletDestroy", 0.333f);
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

        if (collision.CompareTag("Enemy") == true)
        {
            gameObject.SetActive(false);
        }
    }
}

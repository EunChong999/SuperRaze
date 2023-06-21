using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour
{
    GameObject collisonObject;
    bool isCollision;

    private void Start()
    {
        isCollision = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(!isCollision)
            {
                collisonObject = collision.gameObject;
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
                OnDamaged(collision.gameObject);
                Invoke("StartDamage", 1f);
                isCollision = true;
            }
        }
    }

    void OnDamaged(GameObject target)
    {
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);

        Vector2 targetpos = target.transform.position;

        int dirc = gameObject.GetComponentInParent<Transform>().position.x - targetpos.x > 0 ? 1 : -1;

        gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(dirc, 1) * 70, ForceMode2D.Impulse);
    }

    private void StartDamage()
    {
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Physics2D.IgnoreCollision(collisonObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
        isCollision = false;
    }
}

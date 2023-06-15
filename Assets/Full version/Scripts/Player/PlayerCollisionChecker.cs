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
            Debug.Log("Ãæµ¹!");
            collisonObject = collision.gameObject;
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);

            if(!isCollision)
            {
                isCollision = true;
                Invoke("StartDamage", 1f);
            }
        }
    }

    private void StartDamage()
    {
        Physics2D.IgnoreCollision(collisonObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
        isCollision = false;
    }
}

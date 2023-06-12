using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisioner : MonoBehaviour
{
    GameObject collisonObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Ãæµ¹!");
            collisonObject = collision.gameObject;
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
            Invoke("StartDamage", 1f);
        }
    }

    private void StartDamage()
    {
        Physics2D.IgnoreCollision(collisonObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour
{
    public PhysicsMaterial2D physicsMaterial;
    GameObject collisionObject;
    [HideInInspector] public bool isCollision;

    private void Start()
    {
        isCollision = false;
    }

    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isCollision)
            {
                collisionObject = collision.gameObject;
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
                OnDamaged(collision.gameObject);
                isCollision = true;
            }
        }
    }

    void OnDamaged(GameObject target)
    {
        transform.parent.gameObject.layer = 0;
        gameObject.GetComponentInParent<Rigidbody2D>().sharedMaterial = null;
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        Vector2 targetpos = target.GetComponentInParent<Transform>().position;

        int dirc = gameObject.GetComponentInParent<Transform>().position.x - targetpos.x > 0 ? 1 : -1;
        gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(dirc, 1) * 70, ForceMode2D.Impulse);

        Invoke("StartDamage", 0.5f);
    }

    private void StartDamage()
    {
        transform.parent.gameObject.layer = 7;
        gameObject.GetComponentInParent<Rigidbody2D>().sharedMaterial = physicsMaterial;
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Physics2D.IgnoreCollision(collisionObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
        isCollision = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour
{
    public PhysicsMaterial2D physicsMaterial1;
    public PhysicsMaterial2D physicsMaterial2;
    [HideInInspector] public GameObject collisionObject;
    public PlayerController playerController;
    public PlayerTimeSlower playerTimeSlower;
    public PlayerHealther playerHealther;
    [HideInInspector] public bool isCollision;

    private void Start()
    {
        isCollision = false;
        transform.GetComponentInParent<Rigidbody2D>().sharedMaterial = physicsMaterial1;
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
            }
        }
    }

    void OnDamaged(GameObject target)
    {
        isCollision = true;
        playerHealther.Damage();
        transform.parent.gameObject.layer = 0;
        transform.GetComponentInParent<Rigidbody2D>().sharedMaterial = physicsMaterial2;
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);

        if (playerController.horizontal == 0)
        {
            Vector2 targetpos = target.GetComponentInParent<Transform>().position;
            int dirc = gameObject.GetComponentInParent<Transform>().position.x - targetpos.x > 0 ? 1 : -1;
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(dirc, 1) * 70, ForceMode2D.Impulse);
        }

        Invoke("StartDamage", 1);
    }

    public void StartDamage()
    {
        // isSlowering¿Ã æ∆¥“ ∂ß
        if(!playerTimeSlower.isSlowering)
        {
            transform.parent.gameObject.layer = 7;
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        isCollision = false;
        transform.GetComponentInParent<Rigidbody2D>().sharedMaterial = physicsMaterial1;
        Physics2D.IgnoreCollision(collisionObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyCollisionChecker : MonoBehaviour
{
    [HideInInspector] public bool isDamage;
    [SerializeField] AudioSource hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = GameObject.Find("HitOrDead").GetComponent<AudioSource>();

        isDamage = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            hit.Play();
            isDamage = true;
        }
    }
}

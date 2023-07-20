using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingEnemyCollisionChecker : MonoBehaviour
{
    [SerializeField] AudioSource hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = GameObject.Find("HitOrDead").GetComponent<AudioSource>();
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
            gameObject.transform.parent.parent.GetComponent<BombingEnemyExploder>().Bombed();
            gameObject.transform.parent.parent.GetComponent<BombingEnemyExploder>().isBombing = true;
        }
    }
}

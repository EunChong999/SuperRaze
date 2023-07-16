using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyCollisionChecker : MonoBehaviour
{
    [HideInInspector] public bool isDamage;

    // Start is called before the first frame update
    void Start()
    {
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
            isDamage = true;
        }
    }
}

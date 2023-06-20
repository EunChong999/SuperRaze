using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Box"))
        {
            gameObject.SetActive(false);
            //collision.gameObject.GetComponent<Rigidbody2D>().gravityScale /= 1.25f;
        }
    }
}

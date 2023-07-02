using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChange : MonoBehaviour
{
    public Sprite[] currentSprites = new Sprite[3];
    public Sprite[] nextSprites = new Sprite[3];

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("End"))
        {
            if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == currentSprites[0])
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nextSprites[0];
            }
            else if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == currentSprites[1])
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nextSprites[1];
            }
            else if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == currentSprites[2])
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nextSprites[2];
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("End"))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 25;
            Invoke("ReGravity", 1);
        }
    }

    void ReGravity()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 50;
    }
}

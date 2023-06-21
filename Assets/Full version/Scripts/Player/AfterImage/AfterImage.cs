using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float delay;
    private float delaySeconds;
    public GameObject image;
    public bool makeImage;

    // Start is called before the first frame update
    void Start()
    {
        delaySeconds = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(makeImage)
        {
            if (delaySeconds > 0)
            {
                delaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentImage = Instantiate(image, transform.GetChild(0).transform.position, transform.rotation);
                Sprite currentSprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                currentImage.transform.localScale = this.transform.localScale;
                currentImage.GetComponent<SpriteRenderer>().sprite = currentSprite;
                delaySeconds = delay;
                Destroy(currentImage, 1f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public ScreenChange ScreenChange;
    public List<Image> character;
    public List<Sprite> characters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (ScreenChange.CurrentScreenNumber)
        {
            case 2: // 스테이지 1
                character[0].sprite = characters[0];
                break;
            case 4: // 스테이지 2
                character[1].sprite = characters[1];
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteract : MonoBehaviour
{
    public void ButtonSetFalse()
    {
        GetComponent<Button>().interactable = false;

        Invoke(nameof(ButtonSetTrue), 3);
    }

    public void ButtonSetTrue()
    {
        GetComponent<Button>().interactable = true; 
    }
}

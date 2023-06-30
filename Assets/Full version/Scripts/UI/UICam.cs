using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICam : MonoBehaviour
{
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

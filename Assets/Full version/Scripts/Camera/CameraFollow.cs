using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.Follow = GameObject.Find("Player").transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

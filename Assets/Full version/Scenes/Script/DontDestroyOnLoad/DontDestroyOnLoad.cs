using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad dontDestroyOnLoad; // DonDestroyOnLoad 클래스 선언

    private void Awake() // 씬이 시작될 때마다 
    {
        DontDestroyOnLoad(this); // 해당 오브젝트가 씬 전환시에도 파괴되지 않도록 한다.

        if (dontDestroyOnLoad == null) // donDestroyOnLoad가 아직 초기화되지 않았다면
        {
            dontDestroyOnLoad = this; // 현재 인스턴스를 donDestroyOnLoad에 할당하여 다음 씬에서도 유지되도록 한다.
        }
        else // donDestroyOnLoad가 초기화되었다면
        {
            Destroy(gameObject); // 해당 오브젝트를 파괴하여, 씬을 넘어갈 때마다 오브젝트가 중복으로 생성되는 것을 방지한다.
        }
    }
}



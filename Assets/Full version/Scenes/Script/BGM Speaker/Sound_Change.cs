using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound_Change : MonoBehaviour
{
    public Screen_Change Screen_Change;
    public AudioSource AudioSource; 
    public AudioClip Intro_Screen_BGM;
    public AudioClip Stage_01_Screen_BGM;
    public AudioClip Stage_02_Screen_BGM;
    public AudioClip Credit_Screen_BGM;

    private void Start()
    {

    }

    void Update()
    {

    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("화면");
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    //private void OnEnable()
    //{
    //    switch (Screen_Change.CurrentScreenNumber)
    //    {
    //        case 0:
    //            AudioSource.clip = Intro_Screen_BGM;
    //            AudioSource.Play();
    //            break;
    //        case 1:
    //            AudioSource.clip = Intro_Screen_BGM;
    //            AudioSource.Play();
    //            break;
    //        case 2:
    //            AudioSource.clip = Stage_01_Screen_BGM;
    //            AudioSource.Play();
    //            break;
    //        case 3:
    //            AudioSource.clip = Stage_01_Screen_BGM;
    //            AudioSource.Play();
    //            break;
    //        case 4:
    //            AudioSource.clip = Stage_02_Screen_BGM;
    //            AudioSource.Play();
    //            break;
    //        case 5:
    //            AudioSource.clip = Stage_02_Screen_BGM;
    //            AudioSource.Play();
    //            break;
    //        case 6:
    //            AudioSource.clip = Credit_Screen_BGM;
    //            AudioSource.Play();
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private void OnDisable()
    //{
    //    AudioSource.Stop();
    //}
}

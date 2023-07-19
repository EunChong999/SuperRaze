using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] robots = new GameObject[2];
    [SerializeField] private AudioSource audioSource;
    GameObject screenManager;

    // Start is called before the first frame update
    void Start()
    {
        screenManager = GameObject.Find("Screen Manager");
        Invoke("NextScene", 9);
        Invoke("Appear", 6);
        Invoke("Sound", 1);
    }

    private void NextScene()
    {
        screenManager.GetComponent<ScreenChange>().ChangeScreen();
        screenManager.GetComponent<ScreenEffect>().AffectScreen();
        screenManager.GetComponent<ScreenBlock>().BlockScreen();
    }

    void Sound()
    {
        audioSource.Play();
    }

    void Appear()
    {
        robots[0].SetActive(true);
        robots[1].SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private int choice;
    [SerializeField] private GameObject[] choices = new GameObject[4];
    [SerializeField] private AudioSource[] audioSource = new AudioSource[2];
    [SerializeField] private GameObject[] robots = new GameObject[2];
    GameObject screenManager;

    // Start is called before the first frame update
    void Start()
    {
        screenManager = GameObject.Find("Screen Manager");
        Invoke("NextScene", 9);

        switch (choice)
        {
            case 1:
                choices[0].SetActive(true);
                Invoke("FirLivSecLiv", 1);
                StartCoroutine(DisAppear(1, 1));
                break;
            case 2:
                choices[1].SetActive(true);
                Invoke("FirLivSecDea", 1);
                StartCoroutine(DisAppear(1, 0));
                break;
            case 3:
                choices[2].SetActive(true);
                Invoke("FirDeaSecLiv", 1);
                StartCoroutine(DisAppear(0, 1));
                break;
            case 4:
                choices[3].SetActive(true);
                Invoke("FirDeaSecDea", 1);
                StartCoroutine(DisAppear(0, 0));
                break;
                default:
                break;
        }
    }

    IEnumerator DisAppear(int isLiv1, int isLiv2)
    {
        yield return new WaitForSeconds(6);

        if (isLiv1 == 1)
        {
            robots[0].SetActive(true);
        }
        else
        {
            robots[0].SetActive(false);
        }

        if (isLiv2 == 1)
        {
            robots[1].SetActive(true);
        }
        else
        {
            robots[1].SetActive(false);
        }
    }

    private void NextScene()
    {
        screenManager.GetComponent<ScreenChange>().ChangeScreen();
        screenManager.GetComponent<ScreenEffect>().AffectScreen();
        screenManager.GetComponent<ScreenBlock>().BlockScreen();
    }

    void FirLivSecLiv()
    {

    }

    void FirLivSecDea()
    {

        audioSource[1].Play();
    }

    void FirDeaSecLiv()
    {

        audioSource[0].Play();
    }

    void FirDeaSecDea()
    {

        audioSource[0].Play();
        audioSource[1].Play();
    }
}

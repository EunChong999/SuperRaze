using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] public int choice;
    [SerializeField] private GameObject[] choices = new GameObject[4];
    [SerializeField] private AudioSource[] audioSource = new AudioSource[2];
    [SerializeField] private GameObject[] robots = new GameObject[2];
    GameObject screenManager;

    // Start is called before the first frame update
    void Start()
    {
        screenManager = GameObject.Find("Screen Manager");
        Invoke("NextScene", 9);
    }

    private void Update()
    {
        switch (choice)
        {
            case 1:
                if (!choices[0].activeSelf)
                {
                    StartCoroutine(DisAppear(1, 1));
                    choices[0].SetActive(true);
                }
                break;
            case 2:
                if (!choices[1].activeSelf)
                {
                    StartCoroutine(DisAppear(1, 0));
                    choices[1].SetActive(true);
                }
                break;
            case 3:
                if (!choices[2].activeSelf)
                {
                    StartCoroutine(DisAppear(0, 1));
                    choices[2].SetActive(true);
                }
                break;
            case 4:
                if (!choices[3].activeSelf)
                {
                    StartCoroutine(DisAppear(0, 0));
                    choices[3].SetActive(true);
                }
                break;
            default:
                return;
        }
    }

    IEnumerator DisAppear(int isLiv1, int isLiv2)
    {
        yield return new WaitForSeconds(1);

        if (isLiv1 == 1 && isLiv2 == 1)
        {

        }
        else if (isLiv1 == 1 && isLiv2 == 0)
        {
            audioSource[1].Play();
        }
        else if (isLiv1 == 0 && isLiv2 == 1)
        {
            audioSource[0].Play();
        }
        else if (isLiv1 == 0 && isLiv2 == 0)
        {
            audioSource[0].Play();
            audioSource[1].Play();
        }

        yield return new WaitForSeconds(5);

        if (isLiv1 == 1 && isLiv2 == 1) 
        {
            robots[0].SetActive(true);
            robots[1].SetActive(true);
        }
        else if (isLiv1 == 1 && isLiv2 == 0)
        {
            robots[0].SetActive(true);
            robots[1].SetActive(false);
        }
        else if (isLiv1 == 0 && isLiv2 == 1)
        {
            robots[0].SetActive(false);
            robots[1].SetActive(true);
        }
        else if (isLiv1 == 0 && isLiv2 == 0)
        {
            robots[0].SetActive(false);
            robots[1].SetActive(false);
        }
    }

    private void NextScene()
    {
        screenManager.GetComponent<ScreenChange>().ChangeScreen();
        screenManager.GetComponent<ScreenEffect>().AffectScreen();
        screenManager.GetComponent<ScreenBlock>().BlockScreen();
    }
}

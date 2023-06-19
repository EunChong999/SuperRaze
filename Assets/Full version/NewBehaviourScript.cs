using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float interval = 1f; // ���� �����Ǵ� ���� (��)

    private void Start()
    {
        StartCoroutine(DropBalls());
    }

    private IEnumerator DropBalls()
    {
        while (true)
        {
            interval = Random.Range(0, 3);
            Debug.Log(interval);
            yield return new WaitForSeconds(interval);
        }
    }
}



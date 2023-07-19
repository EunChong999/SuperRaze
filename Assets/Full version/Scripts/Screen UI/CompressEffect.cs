using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CompressEffect : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    private Vector3 origin;
    [SerializeField] private float time;

    private void Awake()
    {
        origin = transform.position;
    }

    private void Start()
    {
        Invoke("CompressDown", time);
    }

    private void CompressDown()
    {
        transform.DOMove(target, 5).SetEase(Ease.InOutElastic);
        Invoke("CompressUp", 3);
    }

    private void CompressUp()
    {
        transform.DOMove(origin, 5).SetEase(Ease.InOutElastic);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CompressEffect : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    [SerializeField] private float time;

    private void Awake()
    {

    }

    private void Start()
    {
        Invoke("Compress", time);
    }

    private void Compress()
    {
        transform.DOMove(target, 3).SetEase(Ease.InOutElastic).SetLoops(-1, LoopType.Yoyo);
    }
}

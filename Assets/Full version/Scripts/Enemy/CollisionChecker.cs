using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [HideInInspector] public bool isDamage;
    [SerializeField] AudioSource hit;

    void Start()
    {
        hit = GameObject.Find("HitOrDead").GetComponent<AudioSource>();
        isDamage = false;
    }

    virtual public void Collision()
    {
        hit.Play();
        isDamage = true;
    }
}

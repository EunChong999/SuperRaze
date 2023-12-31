using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class FirstPlayerShooter : MonoBehaviour
{
    // Shoot
    [SerializeField] private bool isShooting;
    [SerializeField] private float coolTime;
    [SerializeField] private static FirstPlayerShooter instance;
    [SerializeField] private Animator animator;

    private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private int amountToPool;

    [SerializeField] private GameObject bulletPrefeb;
    [SerializeField] private Transform bulletPosition;
    private CameraShake cameraShake;
    private AudioSource shoot;

    private void Awake()
    {
        // Shoot
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        shoot = GameObject.Find("PlayerShoot").GetComponent<AudioSource>();

        // ShakeCamera
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
        
        // Shoot
        isShooting = false;    

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject Obj = Instantiate(bulletPrefeb);
            Obj.SetActive(false);
            pooledObjects.Add(Obj);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isShooting && GetComponent<FirstPlayerController>().IsGrounded())
        {
            StartCoroutine(Shoot());
            cameraShake.ShakeCamera(10, 0.1f);
        }
    }

    // GetPooledObject & Shoot
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    IEnumerator Shoot()
    {
        shoot.Play();

        animator.SetTrigger("isShooting");

        GameObject Bullet = GetPooledObject();

        if (Bullet != null)
        {
            Bullet.transform.position = bulletPosition.position;
            Bullet.SetActive(true);
        }

        isShooting = true;

        yield return new WaitForSeconds(coolTime);

        isShooting = false;
    }
}

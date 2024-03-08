using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [HideInInspector] public bool isDamage;
    [SerializeField] AudioSource hit;

    void Start()
    {
        isDamage = false;
    }

    virtual public void Collision()
    {
        hit.Play();
        isDamage = true;
    }
}

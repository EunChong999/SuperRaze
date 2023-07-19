using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerHealther : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider energyBar;
    [SerializeField] private const float maxHealth = 100;
    [SerializeField] private const float maxEnergy = 100;
    [HideInInspector] public float currentHealth = 0;
    [HideInInspector] public float currentEnergy = 0;
    private float targetHealth = 0;
    private float targetEnergy = 0;
    [SerializeField] private float slideSpeed;
    [HideInInspector] public bool isDead;
    Material material;
    [HideInInspector] public bool isDissolving;
    float fade;
    private GameObject screenManager;

    void Start()
    {
        screenManager = GameObject.Find("Screen Manager");

        material = transform.GetChild(0).GetComponent<SpriteRenderer>().material;

        isDead = false;

        fade = 0;
        isDissolving = true;
    }

    private void OnDisable()
    {
        if (screenManager != null && isDead)
        {
            if (!screenManager.GetComponent<ScreenBlock>().on)
            {
                screenManager.GetComponent<ScreenChange>().RestartScreen();
                screenManager.GetComponent<ScreenEffect>().AffectScreen();
                screenManager.GetComponent<ScreenBlock>().BlockScreen();
            }
        }
    }

    private void OnEnable()
    {
        isDead = false;
    }

    void Update()
    {
        if (healthBar == null)
        {
            healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
            healthBar.value = maxHealth;
            currentHealth = maxHealth;
            targetHealth = maxHealth;
        }

        if (energyBar == null) 
        {
            energyBar = GameObject.Find("Energy Bar").GetComponent<Slider>();
            energyBar.value = maxEnergy;
            currentEnergy = maxEnergy;
            targetEnergy = maxEnergy;
        }

        // 체력 및 마나
        if (!screenManager.GetComponent<ScreenTimer>().isTimeStop)
        {
            currentHealth = Mathf.LerpUnclamped(healthBar.value, targetHealth, 1);
            healthBar.value = currentHealth;
            currentEnergy = Mathf.LerpUnclamped(energyBar.value, targetEnergy, 1);
            energyBar.value = currentEnergy;

            if (currentHealth <= 0 && !screenManager.GetComponent<ScreenChange>().on)
            {
                currentHealth = 0;

                isDead = true; // 죽는 시점

                isDissolving = true;
            }

            if (!isDead)
            {
                RecoverHealth();
                RecoverEnergy();
                Spawn();
            }
            else
            {
                Dead();
            }
        }
    }

    // 플레이어 생성
    public void Spawn()
    {
        if(isDissolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1) 
            {
                fade = 1;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }

    // 적으로부터의 데미지
    public void Damage()
    {
        if(!isDead)
        {
            targetHealth -= 25;
        }
    }

    // 플레이어의 점진적 체력 회복
    public void RecoverHealth()
    {
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }
        else
        {
            targetHealth += 0.0125f;
        }
    }

    // 플레이어의 스킬 사용
    public void UseSkill()
    {
        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }
        else
        {
            targetEnergy -= 0.1f;
        }
    }

    // 플레이어의 점진적 마나 회복
    public void RecoverEnergy()
    {
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            targetEnergy += 0.025f;
        }
    }

    // 플레이어 사망
    public void Dead()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0)
            {
                fade = 0;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}

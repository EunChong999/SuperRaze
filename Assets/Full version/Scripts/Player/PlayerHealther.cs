using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private bool isDead;

    void Start()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        energyBar = GameObject.Find("Energy Bar").GetComponent<Slider>();
        healthBar.value = maxHealth;
        energyBar.value = maxEnergy;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        targetHealth = maxHealth;
        targetEnergy = maxEnergy;
    }

    void Update()
    {
        currentHealth = Mathf.LerpUnclamped(healthBar.value, targetHealth, 1);
        healthBar.value = currentHealth;
        currentEnergy = Mathf.LerpUnclamped(energyBar.value, targetEnergy, 1);
        energyBar.value = currentEnergy;
        RecoverHealth();
        RecoverEnergy();
    }

    // 적으로부터의 데미지
    public void Damage()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            targetHealth -= 20;
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
            targetHealth += 0.01f;
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

    }
}

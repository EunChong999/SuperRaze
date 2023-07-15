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
    [HideInInspector] public bool isDead;
    Material material;
    [SerializeField] private Material materialTemp;
    [HideInInspector] public bool isDissolving;
    float fade;
    private ScreenTimer screenTimer;

    void Start()
    {
        screenTimer = GameObject.Find("Screen Manager").GetComponent<ScreenTimer>();
        material = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        energyBar = GameObject.Find("Energy Bar").GetComponent<Slider>();
        healthBar.value = maxHealth;
        energyBar.value = maxEnergy;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        targetHealth = maxHealth;
        targetEnergy = maxEnergy;

        isDead = false;

        fade = 0;
        isDissolving = true;
    }

    void Update()
    {
        // ü�� �� ����
        if (!screenTimer.isTimeStop)
        {
            currentHealth = Mathf.LerpUnclamped(healthBar.value, targetHealth, 1);
            healthBar.value = currentHealth;
            currentEnergy = Mathf.LerpUnclamped(energyBar.value, targetEnergy, 1);
            energyBar.value = currentEnergy;

            if (!isDead)
            {
                RecoverHealth();
                RecoverEnergy();
                Spawn();
            }
        }
    }

    // �÷��̾� ����
    public void Spawn()
    {
        if(isDissolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1) 
            {
                fade = 1;
                transform.GetChild(0).GetComponent<SpriteRenderer>().material = materialTemp;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }

    // �����κ����� ������
    public void Damage()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
            Dead();
        }
        else if(!isDead)
        {
            targetHealth -= 20;
        }
    }

    // �÷��̾��� ������ ü�� ȸ��
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

    // �÷��̾��� ��ų ���
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

    // �÷��̾��� ������ ���� ȸ��
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

    // �÷��̾� ���
    public void Dead()
    {
        if (!isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0)
            {
                fade = 0;
                transform.GetChild(0).GetComponent<SpriteRenderer>().material = material;
                isDissolving = true;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}

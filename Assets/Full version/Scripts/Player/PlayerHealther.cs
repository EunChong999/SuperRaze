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

        // ü�� �� ����
        if (!screenManager.GetComponent<ScreenTimer>().isTimeStop)
        {
            currentHealth = Mathf.LerpUnclamped(healthBar.value, targetHealth, 1);
            healthBar.value = currentHealth;
            currentEnergy = Mathf.LerpUnclamped(energyBar.value, targetEnergy, 1);
            energyBar.value = currentEnergy;

            if (currentHealth <= 0 && !screenManager.GetComponent<ScreenChange>().on)
            {
                currentHealth = 0;

                isDead = true; // �״� ����

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

    // �÷��̾� ����
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

    // �����κ����� ������
    public void Damage()
    {
        if(!isDead)
        {
            targetHealth -= 25;
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
            targetHealth += 0.0125f;
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

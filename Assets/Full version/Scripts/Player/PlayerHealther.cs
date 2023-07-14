using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealther : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider energyBar;
    [SerializeField] private const float maxHealth = 100;
    [SerializeField] private const float maxEnergy = 100;
    private float currentHealth = 0;
    private float currentEnergy = 0;
    private float targetHealth = 0;
    private float targetEnergy = 0;
    [SerializeField] private float slideSpeed;
    private bool isHealthChanging;
    private bool isEnergyChanging;

    // Start is called before the first frame update
    void Start()
    {
        //healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        //energyBar = GameObject.Find("Energy Bar").GetComponent<Slider>();
        healthBar.value = maxHealth;
        energyBar.value = maxEnergy;
        //currentHealth = maxHealth;
        //currentEnergy = maxEnergy;
        targetHealth = maxHealth;
        targetEnergy = maxEnergy;
        isHealthChanging = false;
        isEnergyChanging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHealthChanging)
        {
            RecoverHealth();
        }
        else
        {
            currentHealth = Mathf.SmoothDamp(healthBar.value, targetHealth, ref slideSpeed, 10 * Time.deltaTime);
            healthBar.value = currentHealth;
        }

        if (!isEnergyChanging)
        {
            RecoverEnergy();
        }
        else
        {
            currentEnergy = Mathf.SmoothDamp(energyBar.value, targetEnergy, ref slideSpeed, 10 * Time.deltaTime);
            energyBar.value = currentEnergy;
        }
    }

    // �����κ����� ������
    public void Damage()
    {
        // ���� ü���� 0�� �Ǹ� 
        if (currentHealth == 0)
        {
            // �÷��̾� ��� �Լ� ȣ��
            // Dead();
        }
        // ���� ü���� 0�� �ƴϸ� 
        else
        {
            // ü�� ����
            isHealthChanging = true;
            targetHealth -= 10;

            if (currentHealth == targetHealth)
            {
                isHealthChanging = false;
            }
        }
    }

    // �÷��̾��� ������ ü�� ȸ��
    public void RecoverHealth()
    {
        // ���� �ִ� ü���� ������
        if (currentHealth > maxHealth) 
        {
            // ü���� �ִ� ü������ �ʱ�ȭ
            currentHealth = maxHealth;
        }
        // ���� �ִ� ü���� �Ѿ�� ������ 
        else
        {
            // ü�� ����
            currentHealth = Mathf.SmoothDamp(healthBar.value, maxHealth, ref slideSpeed, 1000 * Time.deltaTime);
            healthBar.value = currentHealth;
        }
    }

    // �÷��̾��� ��ų ���
    public void UseSkill()
    {
        // ���� ������ 0�� �Ǹ�
        if (currentEnergy == 0)
        {
            // ��ų ��� ����
        }
        // ���� ������ 0�� �ƴϸ�
        else
        {
            // ���� ����
            isEnergyChanging = true;
            targetEnergy -= 10;

            if (currentEnergy == targetEnergy)
            {
                isEnergyChanging = false;
            }
        }
    }

    // �÷��̾��� ������ ���� ȸ��
    public void RecoverEnergy()
    {
        // ���� �ִ� ������ ������
        if (currentEnergy > maxEnergy)
        {
            // ������ �ִ� ������ �ʱ�ȭ
            currentEnergy = maxEnergy;
        }
        // ���� �ִ� ü���� �Ѿ�� ������ 
        else
        {
            // ���� ����
            currentEnergy = Mathf.SmoothDamp(energyBar.value, maxEnergy, ref slideSpeed, 10 * Time.deltaTime);
            energyBar.value = currentEnergy;
        }
    }

    // �÷��̾� ���
    public void Dead()
    {
        // �÷��̾� ��� ����Ʈ Ȱ��ȭ
        // �� ���ε� �Լ� ȣ��
    }
}

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

    // 적으로부터의 데미지
    public void Damage()
    {
        // 만약 체력이 0이 되면 
        if (currentHealth == 0)
        {
            // 플레이어 사망 함수 호출
            // Dead();
        }
        // 만약 체력이 0이 아니면 
        else
        {
            // 체력 감소
            isHealthChanging = true;
            targetHealth -= 10;

            if (currentHealth == targetHealth)
            {
                isHealthChanging = false;
            }
        }
    }

    // 플레이어의 점진적 체력 회복
    public void RecoverHealth()
    {
        // 만약 최대 체력을 넘으면
        if (currentHealth > maxHealth) 
        {
            // 체력을 최대 체력으로 초기화
            currentHealth = maxHealth;
        }
        // 만약 최대 체력을 넘어가지 않으면 
        else
        {
            // 체력 증가
            currentHealth = Mathf.SmoothDamp(healthBar.value, maxHealth, ref slideSpeed, 1000 * Time.deltaTime);
            healthBar.value = currentHealth;
        }
    }

    // 플레이어의 스킬 사용
    public void UseSkill()
    {
        // 만약 마나가 0이 되면
        if (currentEnergy == 0)
        {
            // 스킬 사용 금지
        }
        // 만약 마나가 0이 아니면
        else
        {
            // 마나 감소
            isEnergyChanging = true;
            targetEnergy -= 10;

            if (currentEnergy == targetEnergy)
            {
                isEnergyChanging = false;
            }
        }
    }

    // 플레이어의 점진적 마나 회복
    public void RecoverEnergy()
    {
        // 만약 최대 마나를 넘으면
        if (currentEnergy > maxEnergy)
        {
            // 마나를 최대 마나로 초기화
            currentEnergy = maxEnergy;
        }
        // 만약 최대 체력을 넘어가지 않으면 
        else
        {
            // 마나 증가
            currentEnergy = Mathf.SmoothDamp(energyBar.value, maxEnergy, ref slideSpeed, 10 * Time.deltaTime);
            energyBar.value = currentEnergy;
        }
    }

    // 플레이어 사망
    public void Dead()
    {
        // 플레이어 사망 이펙트 활성화
        // 씬 릴로드 함수 호출
    }
}

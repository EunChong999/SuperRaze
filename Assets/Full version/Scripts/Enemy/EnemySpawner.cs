using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }; // ���� ���¿� ���� ���ü��� ���̱� ���� enum ����
                                                            // ���� ������, �����, �������̴�.

    [System.Serializable] // �ν�����â���� Ŭ���� ���� ���� ������ �� �ֵ��� �Ѵ�.
    public class Wave // Wave�� ���� Ŭ���� ����
    {
        public string name; // ���� �̸�
        public Transform enemy; // ���� ����
        public int count; // ���� ����
        public float rate; // ���� ���
    }

    public Wave[] waves; // Wave Ŭ������ ���� �迭 ����
    private int nextWave = 0; // ���� Wave�� �ε��� ��ȣ,
                              // ù ���̺�� ���� ���̺��̹Ƿ� �ε��� ���� 0�̴�.
    private bool isWaveCompleted;

    public Transform[] spawnPoints; // ���� ������ ����Ʈ

    public float timeBetweenWaves = 5f; // ���� Wave�� ���� Wave������ �ð�
    public float waveCountdown; // ���� Wave�� �̸������ ���� �ð�

    private float searchCountdown = 1f; // ���� ���� ���θ� Ȯ���ϴ� �ð�

    public SpawnState currentState = SpawnState.COUNTING; // ù ���� ���´� ����������,
                                                          // ���� ���� �����ϴ� ���� ���� �����Ͽ� ���� ���¸� �Ǵ�

    private void Start() // �ش� ������Ʈ�� ó�� ������ ��
    {
        isWaveCompleted = false;

        if (spawnPoints.Length == 0) // ���� ����Ʈ�� �������� ���� ��
        {
            Debug.LogError("No spawn points referenced."); // ��� �޼��� ���
        }

        waveCountdown = timeBetweenWaves; // WaveCountdown�� timeBetweenwaves�� �Ҵ�
    }

    private void Update() // �� �����Ӹ��� ����
    {
        if (!isWaveCompleted)
        {
            if (currentState == SpawnState.WAITING) // ���� ���°� ������� �� 
            {
                if (!EnemyIsAlive()) // ���� �������� �ʴ´ٸ�
                {
                    WaveCompleted(); // Wave �Ϸ� �Լ� ȣ��
                }
                else // ���� �����Ѵٸ�
                {
                    return; // �ٽ� ó������ �̵�
                }
            }

            if (waveCountdown <= 0) // ���� �ð��� 0�� ���� ��
            {
                if (currentState != SpawnState.SPAWNING) // ���� ���°� �������� �ƴ϶��  
                {
                    StartCoroutine(SpawnWave(waves[nextWave])); // ���� Wave�� ������ �����Ѵ�.
                }
            }
            else // ���� �ð��� 0�� �ƴ϶�� 
            {
                waveCountdown -= Time.deltaTime; // ����Ͽ� ���� �ð��� ���ҽ�Ų��.
            }
        }
    }

    void WaveCompleted() // Wave �Ϸ� �Լ� 
    {
        Debug.Log("Wave Completed!"); // Wave �Ϸ� �޽��� ���

        currentState = SpawnState.COUNTING; // ���� ���¸� ���������� ��ȯ
        waveCountdown = timeBetweenWaves; // WaveCountdown�� timeBetweenwaves�� ���Ҵ�

        if (nextWave + 1 > waves.Length - 1) // ���� Wave�� �������� ���� ��
        {
            isWaveCompleted = true;
            //nextWave = 0; // ���� Wave�� ó������ �ʱ�ȭ
            //Debug.Log("ALL WAVES COMPLETE! Looping..."); // ��� Wave �Ϸ� �޽��� ���  
        }
        else
        {
            nextWave++; // ���� Wave�� �̵�
        }
    }

    bool EnemyIsAlive() // ���� ���� ���� Ȯ��
    {
        searchCountdown -= Time.deltaTime; // ����Ͽ� ���� �ð��� ���ҽ�Ų��.
        if (searchCountdown <= 0f) // ���� �ð��� 0�� ���� ��
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) // ������ ������� �ʴٸ�
            {
                return false; // ������ ��ȯ
            }
        }

        return true; // ���� ��ȯ
    }

    IEnumerator SpawnWave(Wave wave) // Wave Ŭ������ �Ű������� �Ͽ� Wave�� ��ȯ��Ű�� SpawnWave �ڷ�ƾ ���� 
    {
        Debug.Log("Spawning Wave : " + wave.name);
        currentState = SpawnState.SPAWNING; // ���� ���¸� ���������� ��ȯ

        for (int i = 0; i < wave.count; i++) // �ش� Wave�� ���� ���ڸ�ŭ �ݺ��Ͽ�
        {
            SpawnEnemy(wave.enemy); // �ش� Wave�� ���� �����Ѵ�.
            yield return new WaitForSeconds(1f / wave.rate); // ����� ��������(��������) �� �ʰ� �����ȴ�.
        }

        currentState = SpawnState.WAITING; // ������ �������Ƿ� ���� ���¸� ��������� ��ȯ

        yield break; // �ڷ�ƾ Ż��
    }

    void SpawnEnemy(Transform enemy) // Wave Ŭ������ ���� �Ű������� �Ͽ� ���� �����ϴ� �Լ�
    {
        Debug.Log("Spawning Enemy : " + enemy.name); // ������ ���� �̸��� ���

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)]; // ���� ����Ʈ�� �������� �Ҵ�
        Instantiate(enemy, sp.position, sp.rotation); // �������� �Ҵ�� ��ġ�� ���� ��ġ�� �Ҵ�
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }; // ���� ���¿� ���� ���ü��� ���̱� ���� enum ����
                                                            // ���� ������, �����, �������̴�.

    [System.Serializable] // �ν�����â���� Ŭ���� ���� ���� ������ �� �ֵ��� �Ѵ�.
    public class Wave // Wave�� ���� Ŭ���� ����
    {
        public string name; // ���� �̸�
        public Transform[] enemy; // ���� ����
        [HideInInspector] public GameObject enemyBody; // ������ ��
        public int count; // ���� ����
        public float rate; // ���� ���
    }

    public Wave[] waves; // Wave Ŭ������ ���� �迭 ����
    private int nextWave = 0; // ���� Wave�� �ε��� ��ȣ,
                              // ù ���̺�� ���� ���̺��̹Ƿ� �ε��� ���� 0�̴�.
    private bool isWaveCompleted;
    private bool isSpawnEnd;

    public Transform[] spawnPoints; // ���� ������ ����Ʈ

    public float timeBetweenWaves = 5f; // ���� Wave�� ���� Wave������ �ð�
    public float waveCountdown; // ���� Wave�� �̸������ ���� �ð�

    private float searchCountdown = 1f; // ���� ���� ���θ� Ȯ���ϴ� �ð�

    public SpawnState currentState = SpawnState.COUNTING; // ù ���� ���´� ����������,
                                                          // ���� ���� �����ϴ� ���� ���� �����Ͽ� ���� ���¸� �Ǵ�

    private GameObject screenManager;

    private void Start() // �ش� ������Ʈ�� ó�� ������ ��
    {
        screenManager = GameObject.Find("Screen Manager");

        isWaveCompleted = false;
        isSpawnEnd = false;

        if (spawnPoints.Length == 0) // ���� ����Ʈ�� �������� ���� ��
        {
            Debug.LogError("No spawn points referenced."); // ��� �޼��� ���
        }

        waveCountdown = timeBetweenWaves; // WaveCountdown�� timeBetweenwaves�� �Ҵ�
    }

    private void OnDisable()
    {
        if (screenManager != null && isWaveCompleted)
        {
            if (!screenManager.GetComponent<ScreenBlock>().on)
            {
                screenManager.GetComponent<ScreenChange>().ChangeScreen();
                screenManager.GetComponent<ScreenEffect>().AffectScreen();
                screenManager.GetComponent<ScreenBlock>().BlockScreen();
            }
        }
    }

    private void OnEnable()
    {
        isWaveCompleted = false;
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

        if (isWaveCompleted && !isSpawnEnd)
        {
            Invoke("SpawnEnd", 1);
            isSpawnEnd = true;
        }
    }

    void SpawnEnd()
    {
        gameObject.SetActive(false); // ���� ��Ȱ��ȭ
    }

    void WaveCompleted() // Wave �Ϸ� �Լ� 
    {
        Debug.Log("Wave Completed!"); // Wave �Ϸ� �޽��� ���

        currentState = SpawnState.COUNTING; // ���� ���¸� ���������� ��ȯ
        waveCountdown = timeBetweenWaves; // WaveCountdown�� timeBetweenwaves�� ���Ҵ�

        if (nextWave + 1 > waves.Length - 1) // ���� Wave�� �������� ���� ��
        {
            if (!screenManager.GetComponent<ScreenChange>().on) 
            {
                isWaveCompleted = true; // Wave ����
            }

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
            int randomEnemy = Random.Range(0, wave.enemy.Length); // �־��� �� �߿��� �������� ������ ���� ���Ѵ�.
            SpawnEnemy(wave.enemy[randomEnemy], wave.enemyBody); // �ش� Wave�� ���� �����Ѵ�.
            yield return new WaitForSeconds(/*1f / wave.rate*/ 0); // ����� ��������(��������) �� �ʰ� �����ȴ�.
        }

        currentState = SpawnState.WAITING; // ������ �������Ƿ� ���� ���¸� ��������� ��ȯ

        yield break; // �ڷ�ƾ Ż��
    }

    void SpawnEnemy(Transform enemy, GameObject enemyBody) // Wave Ŭ������ ���� �Ű������� �Ͽ� ���� �����ϴ� �Լ�
    {
/*        Debug.Log("Spawning Enemy : " + enemy.name);*/ // ������ ���� �̸��� ���

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)]; // ���� ����Ʈ�� �������� �Ҵ�
        enemyBody = Instantiate(enemy, sp.position, sp.rotation).gameObject; // �������� �Ҵ�� ��ġ�� ���� ��ġ�� �Ҵ�
        enemyBody.transform.SetParent(sp); // ������ ���� �θ� ���� ����Ʈ�� ����
    }
}

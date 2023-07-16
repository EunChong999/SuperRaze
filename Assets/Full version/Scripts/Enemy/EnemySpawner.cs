using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }; // 스폰 상태에 대한 가시성을 높이기 위해 enum 선언
                                                            // 각각 생성중, 대기중, 집계중이다.

    [System.Serializable] // 인스펙터창에서 클래스 내의 값을 변경할 수 있도록 한다.
    public class Wave // Wave에 대한 클래스 선언
    {
        public string name; // 적의 이름
        public Transform[] enemy; // 적의 상태
        [HideInInspector] public GameObject enemyBody; // 생성된 적
        public int count; // 적의 숫자
        public float rate; // 적의 등급
    }

    public Wave[] waves; // Wave 클래스에 대한 배열 선언
    private int nextWave = 0; // 다음 Wave의 인덱스 번호,
                              // 첫 웨이브는 다음 웨이브이므로 인덱스 값은 0이다.
    private bool isWaveCompleted;
    private bool isSpawnEnd;

    public Transform[] spawnPoints; // 적이 스폰될 포인트

    public float timeBetweenWaves = 5f; // 현재 Wave와 다음 Wave까지의 시간
    public float waveCountdown; // 다음 Wave에 이르기까지 남은 시간

    private float searchCountdown = 1f; // 적의 생존 여부를 확인하는 시간

    public SpawnState currentState = SpawnState.COUNTING; // 첫 현재 상태는 집계중으로,
                                                          // 현재 씬에 존재하는 적의 수를 집계하여 다음 상태를 판단

    private GameObject screenManager;

    private void Start() // 해당 오브젝트가 처음 생성될 때
    {
        screenManager = GameObject.Find("Screen Manager");

        isWaveCompleted = false;
        isSpawnEnd = false;

        if (spawnPoints.Length == 0) // 스폰 포인트가 존재하지 않을 때
        {
            Debug.LogError("No spawn points referenced."); // 경고 메세지 출력
        }

        waveCountdown = timeBetweenWaves; // WaveCountdown에 timeBetweenwaves를 할당
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

    private void Update() // 매 프레임마다 실행
    {
        if (!isWaveCompleted)
        {
            if (currentState == SpawnState.WAITING) // 현재 상태가 대기중일 때 
            {
                if (!EnemyIsAlive()) // 적이 존재하지 않는다면
                {
                    WaveCompleted(); // Wave 완료 함수 호출
                }
                else // 적이 존재한다면
                {
                    return; // 다시 처음으로 이동
                }
            }

            if (waveCountdown <= 0) // 남은 시간이 0이 됐을 때
            {
                if (currentState != SpawnState.SPAWNING) // 현재 상태가 생성중이 아니라면  
                {
                    StartCoroutine(SpawnWave(waves[nextWave])); // 다음 Wave의 스폰을 시작한다.
                }
            }
            else // 남은 시간이 0이 아니라면 
            {
                waveCountdown -= Time.deltaTime; // 계속하여 남은 시간을 감소시킨다.
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
        gameObject.SetActive(false); // 스폰 비활성화
    }

    void WaveCompleted() // Wave 완료 함수 
    {
        Debug.Log("Wave Completed!"); // Wave 완료 메시지 출력

        currentState = SpawnState.COUNTING; // 현재 상태를 집계중으로 전환
        waveCountdown = timeBetweenWaves; // WaveCountdown에 timeBetweenwaves를 재할당

        if (nextWave + 1 > waves.Length - 1) // 다음 Wave가 존재하지 않을 때
        {
            if (!screenManager.GetComponent<ScreenChange>().on) 
            {
                isWaveCompleted = true; // Wave 종료
            }

            //nextWave = 0; // 다음 Wave를 처음으로 초기화
            //Debug.Log("ALL WAVES COMPLETE! Looping..."); // 모든 Wave 완료 메시지 출력  
        }
        else
        {
            nextWave++; // 다음 Wave로 이동
        }
    }

    bool EnemyIsAlive() // 적의 생존 여부 확인
    {
        searchCountdown -= Time.deltaTime; // 계속하여 남은 시간을 감소시킨다.
        if (searchCountdown <= 0f) // 남은 시간이 0이 됐을 때
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) // 적들이 살아있지 않다면
            {
                return false; // 거짓을 반환
            }
        }

        return true; // 참을 반환
    }

    IEnumerator SpawnWave(Wave wave) // Wave 클래스를 매개변수로 하여 Wave를 순환시키는 SpawnWave 코루틴 선언 
    {
        Debug.Log("Spawning Wave : " + wave.name);
        currentState = SpawnState.SPAWNING; // 현재 상태를 생성중으로 전환

        for (int i = 0; i < wave.count; i++) // 해당 Wave의 적의 숫자만큼 반복하여
        {
            int randomEnemy = Random.Range(0, wave.enemy.Length); // 주어진 적 중에서 랜덤으로 생성할 적을 정한다.
            SpawnEnemy(wave.enemy[randomEnemy], wave.enemyBody); // 해당 Wave의 적을 생성한다.
            yield return new WaitForSeconds(/*1f / wave.rate*/ 0); // 등급이 높을수록(작을수록) 더 늦게 생성된다.
        }

        currentState = SpawnState.WAITING; // 생성이 끝났으므로 현재 상태를 대기중으로 전환

        yield break; // 코루틴 탈출
    }

    void SpawnEnemy(Transform enemy, GameObject enemyBody) // Wave 클래스의 적을 매개변수로 하여 적을 생성하는 함수
    {
/*        Debug.Log("Spawning Enemy : " + enemy.name);*/ // 생성된 적의 이름을 출력

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)]; // 스폰 포인트를 무작위로 할당
        enemyBody = Instantiate(enemy, sp.position, sp.rotation).gameObject; // 무작위로 할당된 위치를 적의 위치에 할당
        enemyBody.transform.SetParent(sp); // 생성된 적의 부모를 스폰 포인트로 지정
    }
}

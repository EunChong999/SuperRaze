using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Ǯ Ŭ����
    [System.Serializable]
    public class Pool
    {
        // �±�
        public string tag;

        // ������
        public GameObject prefeb;

        // ũ��
        public int size;
    }

    #region singleton

    // Ŭ������ �ν��Ͻ�
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // Ǯ ����Ʈ
    [SerializeField]
    private List<Pool> pools;

    // �̸��� ť�� ���� �̷�� ��ųʸ� 
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        // Ǯ �ʱ�ȭ
        InitPool();
    }

    private void InitPool()
    {
        // Ǯ ��ųʸ��� �ν��Ͻ� ����
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // Ǯ ����Ʈ �ȿ� �ִ� Ǯ ������
        foreach (Pool pool in pools)
        {
            // ������Ʈ Ǯ�� �ν��Ͻ� ����
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // ������ ���� �ݺ�
            for (int i = 0; i < pool.size; i++)
            {
                // ������ ��ü ����
                GameObject obj = Instantiate(pool.prefeb);

                // ��ü ��Ȱ��ȭ
                obj.SetActive(false);

                // ������Ʈ Ǯ�� ��ü ����
                objectPool.Enqueue(obj);
            }

            // Ǯ ��ųʸ��� �±׿� �Բ� ������Ʈ Ǯ ����
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // Ǯ ��ųʸ��� �ش� �±װ� ���� ���
        if (!poolDictionary.ContainsKey(tag))
        {
            // ��� �޽��� ���
            Debug.LogWarning("Pool with tag " + tag + "doesn't excist.");

            // �ΰ� ��ȯ
            return null;
        }

        // ���� ������Ʈ�� Ǯ ����
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // ���� ������Ʈ Ȱ��ȭ
        objectToSpawn.SetActive(true);

        // ���� ������Ʈ ��ġ ����
        objectToSpawn.transform.position = position;

        // ���� ������Ʈ ȸ�� ����
        objectToSpawn.transform.rotation = rotation;

        // ���� ������Ʈ�� �Լ� ���� �� ����
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        // Ǯ���� ������Ʈ�� ���� �Լ��� �Ҵ�Ǿ��� ���
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        // ���� ������Ʈ�� Ǯ ��ųʸ��� ����
        poolDictionary[tag].Enqueue(objectToSpawn);

        // ���� ������Ʈ ��ȯ
        return objectToSpawn;
    }
}

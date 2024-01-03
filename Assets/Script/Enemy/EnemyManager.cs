using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<Enemy> poolingObjectQueue = new Queue<Enemy>();

    private void Awake()
    {
        Instance = this;

        Initialize(10);

    }


    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Enemy CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Enemy>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Enemy GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Enemy obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

    public void Start()
    {
        StartCoroutine("Spawn");
    }
    public IEnumerator Spawn()
    {
        while (true)
        {
            var enemy = GetObject();
            yield return new WaitForSeconds(Random.Range(1, 5));
        }

    }
}
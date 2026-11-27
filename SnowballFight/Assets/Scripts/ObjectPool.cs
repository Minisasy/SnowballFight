using Mirror;
using UnityEngine;

public class ObjectPool : NetworkBehaviour
{
    public static ObjectPool singleton;

    [Header("Settings")]
    public GameObject prefab;

    [Header("Debug")]
    public int currentCount;
    public Pool<GameObject> pool;

    void Start()
    {
        InitializePool();
        singleton = this;
        NetworkClient.RegisterPrefab(prefab, SpawnHandler, UnspawnHandler);
    }

    // used by NetworkClient.RegisterPrefab
    GameObject SpawnHandler(SpawnMessage msg) => Get(msg.position, msg.rotation);

    // used by NetworkClient.RegisterPrefab
    void UnspawnHandler(GameObject spawned) => Return(spawned);

    void OnDestroy()
    {
        NetworkClient.UnregisterPrefab(prefab);
    }

    void InitializePool()
    {
        // create pool with generator function
        pool = new Pool<GameObject>(CreateNew, 5);
    }

    GameObject CreateNew()
    {
        // use this object as parent so that objects dont crowd hierarchy
        GameObject next = Instantiate(prefab, transform);
        next.name = $"{prefab.name}_pooled_{currentCount}";
        next.SetActive(false);
        currentCount++;
        return next;
    }

    // Used to take Object from Pool.
    // Should be used on server to get the next Object
    // Used on client by NetworkClient to spawn objects
    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject next = pool.Get();

        // set position/rotation and set active
        next.transform.position = position;
        next.transform.rotation = rotation;
        next.SetActive(true);
        return next;
    }

    // Used to put object back into pool so they can b
    // Should be used on server after unspawning an object
    // Used on client by NetworkClient to unspawn objects
    public void Return(GameObject spawned)
    {
        // disable object
        spawned.SetActive(false);

        // add back to pool
        pool.Return(spawned);
    }


    //[SerializeField] GameObject[] objects;

    //private int itemCount;

    //public void SpawnSnowball(Transform snowballTransform, int spawnItemCount, out GameObject lastSpawned)
    //{
    //    lastSpawned = null;
    //    for (int i = 0; i < objects.Length; i++)
    //    {
    //        if (objects[i].activeInHierarchy == false)
    //        {
    //            objects[i].transform.position = snowballTransform.position;
    //            objects[i].transform.rotation = snowballTransform.rotation;

    //            //ActivateSnowball(objects[i]);
    //            objects[i].SetActive(true);

    //            itemCount++;
    //            if (itemCount >= spawnItemCount)
    //            {
    //                itemCount = 0;
    //                lastSpawned = objects[i];
    //                return;
    //            }
    //        }
    //    }
    //}

    //[ClientRpc]
    //private void ActivateSnowball(GameObject ball)
    //{
    //    ball.SetActive(true);
    //}
}

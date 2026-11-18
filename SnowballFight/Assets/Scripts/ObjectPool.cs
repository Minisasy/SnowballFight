using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

    private int itemCount;
    private GameObject lastActivatedObject;

    public void SpawnRock(Transform rockTransform, int spawnItemCount)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].activeInHierarchy == false)
            {
                objects[i].transform.position = rockTransform.position;
                objects[i].transform.rotation = rockTransform.rotation;

                objects[i].SetActive(true);

                lastActivatedObject = objects[i];

                itemCount++;
                if (itemCount >= spawnItemCount)
                {
                    itemCount = 0;
                    return;
                }
            }
        }
    }

    public GameObject GetLastSpawned()
    {
        return lastActivatedObject;
    }
}

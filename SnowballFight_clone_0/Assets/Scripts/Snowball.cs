using Mirror;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private float timeRemaining = 0f;

    [SerializeField] float timer = 10f;

    private bool timerFinished = false;
    private void OnEnable()
    {
        timeRemaining = timer;
        timerFinished = false;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;

            if (!timerFinished)
            {
                timerFinished = true;
                DestroySelf();
            }
        }
    }

    [Server]
    void DestroySelf()
    {
        // return to prefab pool
        NetworkServer.UnSpawn(gameObject);
        ObjectPool.singleton.Return(gameObject);
    }
}

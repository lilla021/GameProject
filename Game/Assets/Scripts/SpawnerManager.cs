using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    float spawnRateMin;
    [SerializeField]
    float spawnRateMax;

    private float spawningRate;
    private float next = 0;

    public GameObject spawner;

    public Rigidbody2D triggerBody;

    [SerializeField]
    float maxSpawnOffsetX;
    [SerializeField]
    float maxSpawnOffsetY;
    [SerializeField]
    float maxSpawnOffsetZ;

    [SerializeField]
    GameObject spawnPrefab;

    void Start()
    {
        if (gameObject.CompareTag("RockManager"))
        {
            spawner = GameObject.FindGameObjectWithTag("RockSpawner");
        }
        /*
        if (gameObject.CompareTag("EnemyManager"))
        {
            treasureSpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        }
        */

    }

    /*
    void Update()
    {

        // Spawn a new treasure
        if (Time.time > next)
        {
            float spawnX = Random.Range(-maxSpawnOffsetX, maxSpawnOffsetX);
            float spawnY = Random.Range(-maxSpawnOffsetY, maxSpawnOffsetY);
            float spawnZ = Random.Range(-maxSpawnOffsetZ, maxSpawnOffsetZ);
            Vector3 offset = new Vector3(spawnX, spawnY, spawnZ);
            Vector3 spawnLocation = spawner.transform.position + offset;

            GameObject prefab = Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);

            spawningRate = Random.Range(spawnRateMin, spawnRateMax);
            next = Time.time + spawningRate;
        }

    }
    */

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (triggerBody == null) return;

        if(collision.attachedRigidbody == triggerBody)
        {
            if (Time.time > next)
            {
                float spawnX = Random.Range(-maxSpawnOffsetX, maxSpawnOffsetX);
                float spawnY = Random.Range(-maxSpawnOffsetY, maxSpawnOffsetY);
                float spawnZ = Random.Range(-maxSpawnOffsetZ, maxSpawnOffsetZ);
                Vector3 offset = new Vector3(spawnX, spawnY, spawnZ);
                Vector3 spawnLocation = spawner.transform.position + offset;

                GameObject prefab = Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);

                spawningRate = Random.Range(spawnRateMin, spawnRateMax);
                next = Time.time + spawningRate;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");
        foreach (GameObject slime in slimes)
        {
            Destroy(slime);
        }
    }

}

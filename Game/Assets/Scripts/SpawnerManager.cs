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

    [SerializeField]
    float maxSpawnOffsetX;
    [SerializeField]
    float maxSpawnOffsetY;

    [SerializeField]
    GameObject spawnPrefab;
    List<GameObject> slimes;

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
        slimes = new List<GameObject>();
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
        if(collision.CompareTag("Player"))
        {
            if (Time.time > next)
            {
                float spawnX = Random.Range(-maxSpawnOffsetX, maxSpawnOffsetX);
                float spawnY = Random.Range(-maxSpawnOffsetY, maxSpawnOffsetY);
                Vector3 offset = new Vector3(spawnX, spawnY, 0);
                Vector3 spawnLocation = spawner.transform.position + offset;

                GameObject prefab = Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);
                slimes.Add(prefab);

                spawningRate = Random.Range(spawnRateMin, spawnRateMax);
                next = Time.time + spawningRate;
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            foreach (GameObject slime in slimes) {
                Destroy(slime);
            }
            slimes.Clear();
        }
    }

    public void SetRate(float min, float max)
    {
        if (100 - spawnRateMin < 1)
        {
            spawnRateMin = min;
            spawnRateMax = max;
            spawningRate = Random.Range(spawnRateMin, spawnRateMax);
            next = spawningRate;
            Debug.Log("Chanaged spawning rate");
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private float spawnInterval = 2f;
    [SerializeField]
    private float spawnSize = 1f;
    
    private void Start()
    {
        InvokeRepeating("SpawnCoin", 0f, spawnInterval);
    }
    
    void SpawnCoin()
    {
        float x = Random.Range(-6.8f, 6.8f);
        float y = 0f;
        float z = Random.Range(-3.8f, 3.8f);
        Vector3 spawnPosition = new Vector3(x, y, z);
        GameObject spawnedCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        spawnedCoin.transform.localScale = new Vector3(spawnSize, spawnSize, spawnSize);
    }
}
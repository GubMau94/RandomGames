using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab, coinsPrefab;
    private Vector3 spawnPositionObstacle, spawnPosCoins;

    [SerializeField] private int spawnTimer;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ObstacleSpawner", 2, spawnTimer);        
    }

    private void ObstacleSpawner()
    {
        spawnPositionObstacle = new Vector3(transform.position.x, Random.Range(-2.90f, 2.0f), transform.position.z);
        spawnPosCoins = new Vector3((transform.position.x) - 1f, Random.Range(-2.90f, 2.0f), transform.position.z);

        Instantiate(obstaclePrefab, spawnPositionObstacle, Quaternion.identity);
        Instantiate(coinsPrefab, spawnPosCoins, Quaternion.identity);
    }     

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    private PlayerHealth playerHP;
    public PlayerMovement player;
    public Enemy enemyPrefab;
    public float spawnRad = 10 ;
    public int waveNumber = 1;
    public int enemyCount;
    
    

     //Start is called before the first frame update        
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        playerHP = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
   
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);

            playerHP.health.HPregen(5);
        }
    }

    private Vector3 GenSpawnPos()
    {
        Vector2 spawnPos = GameObject.Find("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRad;

        return spawnPos;
    }
    

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++){
            Enemy enemy = Instantiate(enemyPrefab, GenSpawnPos(), Quaternion.identity);
            enemy.SetPlayer(player.transform);
            enemy.moveSpeed += waveNumber * 0.2f;
            enemy.dmg += waveNumber;
            if (enemy.moveSpeed > enemy.maxSpeed)
            {
                enemy.moveSpeed = enemy.maxSpeed;
            }
        }
    }
}


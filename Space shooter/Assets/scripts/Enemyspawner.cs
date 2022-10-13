using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField] WaveConfig[] WaveConfigs;
    [SerializeField] int MaxEnemyNumber = 100;
    static bool isRunning = false;

    int actWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartSpawns();
    }

    void StartSpawns()
    {
        Debug.Log("Start enemy spawner");
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        foreach (var config in WaveConfigs)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(config));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        int enemyNum = currentWave.GetNumberOfEnemies();
        while (enemyNum > 0)
        {
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
            if (Enemy.Count > MaxEnemyNumber)
                continue;
            enemyNum--;
            var enemy = Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].position,
                Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);
        }
    }
}

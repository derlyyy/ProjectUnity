using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    private int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemies < maxEnemies)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0f);
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            currentEnemies++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
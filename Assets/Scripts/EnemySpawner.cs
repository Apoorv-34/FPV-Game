using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints; // Array of locations where zombies can appear
    
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public int difficultyLevel = 1; // You can change this via your menu later

    void Update()
    {
        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    void SpawnWave()
    {
        // Difficulty determines how many zombies spawn per wave
        for (int i = 0; i < (difficultyLevel * 1); i++)
        {
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        // Pick a random spawn point from your array
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(zombiePrefab, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation);
    }
}
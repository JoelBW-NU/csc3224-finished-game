using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    int maxEnemies = 5;

    [SerializeField]
    float maxRange = 100;

    [SerializeField]
    float minRange = 60;

    [SerializeField]
    float minTimer = 5;

    [SerializeField]
    float maxTimer = 10;

    float timeToSpawn = 0;
    float spawnCounter = 0;

    [SerializeField]
    Transform player;

    [SerializeField]
    GameLogic game;

    List<GameObject> activeEnemies;

    Difficulty difficulty;

    void Start()
    {
        activeEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.isPlaying)
        {
            spawnCounter += Time.deltaTime;
            if (spawnCounter > timeToSpawn && activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        float x = Random.value <= 0.5 ? Random.Range(-maxRange, -minRange) : Random.Range(minRange, maxRange);
        float y = Random.value <= 0.5 ? Random.Range(-maxRange, -minRange) : Random.Range(minRange, maxRange);
        Vector2 position = new Vector2(player.position.x + x, player.position.y + y);
        Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();
        enemy.spawner = this;
        enemy.game = game;
        enemy.SetDifficulty(difficulty);
        activeEnemies.Add(enemy.gameObject);
        timeToSpawn = Random.Range(minTimer, maxTimer);
        spawnCounter = 0;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public void RemoveAllEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            Destroy(enemy);
        }
        activeEnemies.Clear();
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        maxEnemies = difficulty.maxEnemies;
        this.difficulty = difficulty;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDirector : MonoBehaviour {
    
    private static EnemyDirector instance;
    public static EnemyDirector Instance {
        get {
            if (instance == null) Debug.LogError("GameManager missing.");
            return instance;
        }
    }

    public static bool IsActive;
    
    public GameObject[] gruntPrefabs;
    public GameObject[] bossPrefabs;

    private int baseMaxEnemies = 200;

    [SerializeField] private int maxEnemies;
    [SerializeField] private int maxEnemiesIncrementInterval = 2; // seconds
    [SerializeField] private float gruntSpawnRate = 1; // per second
    [SerializeField] private float bossSpawnInterval = 30; // seconds

    [SerializeField] private float enemyMovementSpeed = 0.03f;
    [SerializeField] private int enemyMoveSpeedIncreaseInterval = 60; // seconds

    private PlayerManager playerReference;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public List<GameObject> SpawnedEnemies => spawnedEnemies;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {

        maxEnemies = baseMaxEnemies;
        playerReference = FindObjectOfType<PlayerManager>();

        Debug.Assert(playerReference);
        Debug.Assert(gruntPrefabs.Length != 0); 
        Debug.Assert(bossPrefabs.Length != 0); 

        StartCoroutine("GruntSpawner");
        StartCoroutine("BossSpawner");
        StartCoroutine("EnemyLimitIncreaseTimer");
        StartCoroutine("EnemyMoveSpeedIncreaseTimer");
    }

    IEnumerator GruntSpawner() {
        while(true) {
            if (playerReference == null) playerReference = FindObjectOfType<PlayerManager>();
            if (!IsActive)
            {
                yield return null;
                continue;
            }
            
            yield return new WaitForSeconds(1 / gruntSpawnRate);

            if (spawnedEnemies.Count < maxEnemies && playerReference != null) {

                Vector2 randomPoint = Random.insideUnitCircle * 25;
                Vector2 playerPoint = new Vector2(playerReference.transform.position.x, playerReference.transform.position.y);
                while (Vector2.Distance(randomPoint,playerPoint) < 15) {
                    randomPoint = Random.insideUnitCircle * 25;
                }
                Vector2 spawnPoint = new Vector2(playerPoint.x + randomPoint.x, playerPoint.y + randomPoint.y);

                GameObject newEnemy = Instantiate(gruntPrefabs[Random.Range(0, gruntPrefabs.Length)], spawnPoint, Quaternion.identity);
                EnemyGruntController controller = newEnemy.GetComponent<EnemyGruntController>();
                controller.MovementSpeed = enemyMovementSpeed;
                controller.SetDirector(this);
                spawnedEnemies.Add(newEnemy);
                gruntSpawnRate += 0.02f; // Gradual spawn rate increase
            }
        }
    }

    IEnumerator BossSpawner() {
        while (true) {
            if (playerReference == null) playerReference = FindObjectOfType<PlayerManager>();
            if (!IsActive) {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(bossSpawnInterval);
            
            if (!IsActive)
            {
                yield return null;
                continue;
            }

            if (spawnedEnemies.Count < maxEnemies) {

                Vector2 randomPoint = Random.insideUnitCircle * 25;
                Vector2 playerPoint = new Vector2(playerReference.transform.position.x, playerReference.transform.position.y);
                while (Vector2.Distance(randomPoint, playerPoint) < 15) {
                    randomPoint = Random.insideUnitCircle * 25;
                }
                Vector2 spawnPoint = new Vector2(playerPoint.x + randomPoint.x, playerPoint.y + randomPoint.y);

                GameObject newEnemy = Instantiate(bossPrefabs[Random.Range(0, gruntPrefabs.Length)], spawnPoint, Quaternion.identity);
                EnemyBossController controller = newEnemy.GetComponent<EnemyBossController>();
                controller.MovementSpeed = enemyMovementSpeed / 3;
                controller.SetDirector(this);
                spawnedEnemies.Add(newEnemy);
            }

        }
    }

    IEnumerator EnemyLimitIncreaseTimer() {
        while (true) {
            yield return new WaitForSeconds(maxEnemiesIncrementInterval);
            maxEnemies++;
        }
    }
    IEnumerator EnemyMoveSpeedIncreaseTimer() {
        while (true) {
            yield return new WaitForSeconds(enemyMoveSpeedIncreaseInterval);
            enemyMovementSpeed += 0.01f;
        }
    }

    public void RemoveEnemy(GameObject go) {
        spawnedEnemies.Remove(go);
    }

    public GameObject FindClosestEnemy(Vector3 position) {
        
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in spawnedEnemies) {
            if (enemy == null) continue;

            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }


}

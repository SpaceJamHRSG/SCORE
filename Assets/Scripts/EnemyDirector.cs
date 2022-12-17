using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour {

    public GameObject[] gruntPrefabs;
    public GameObject[] bossPrefabs;

    private int maxBaseEnemies = 30;

    private int maxEnemies;
    private int maxEnemiesIncrementInterval = 5; // seconds
    private float spawnRate = 1; // per second

    private PlayerManager playerReference;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        maxEnemies = maxBaseEnemies;
        playerReference = FindObjectOfType<PlayerManager>();

        Debug.Assert(playerReference);
        Debug.Assert(gruntPrefabs.Length != 0); 
        Debug.Assert(bossPrefabs.Length != 0); 

        StartCoroutine("GruntSpawner");
        StartCoroutine("EnemyLimitIncreaseTimer");
    }

    IEnumerator GruntSpawner() {
        while(true) {
            if (playerReference == null) playerReference = FindObjectOfType<PlayerManager>();

            yield return new WaitForSeconds(1 / spawnRate);

            if (spawnedEnemies.Count < maxEnemies) {

                Vector2 randomPoint = Random.insideUnitCircle * 20;
                Vector2 playerPoint = new Vector2(playerReference.transform.position.x, playerReference.transform.position.y);
                Vector2 spawnPoint = new Vector2(playerPoint.x + randomPoint.x, playerPoint.y + randomPoint.y);

                spawnedEnemies.Add(
                    Instantiate(
                        gruntPrefabs[Random.Range(0, gruntPrefabs.Length)], 
                        spawnPoint, 
                        Quaternion.identity)
                    );
                spawnRate += 0.01f; // Gradual spawn rate increase
            }
        }

    }

    IEnumerator EnemyLimitIncreaseTimer() {
        while (true) {
            yield return new WaitForSeconds(maxEnemiesIncrementInterval);
            maxEnemies++;
        }
    }



}

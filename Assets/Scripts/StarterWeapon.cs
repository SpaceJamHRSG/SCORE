using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWeapon : Weapon {

    public GameObject particlePrefab;
    private float attackRate;

    void Start() {
        attackRate = 1.2f;
        damageMultiplier = 1;

        StartCoroutine("WeaponAutofire");
    }

    IEnumerator WeaponAutofire() {
        while (true) {
            yield return new WaitForSeconds(1 / attackRate);
            Vector2 playerPosition = FindObjectOfType<PlayerManager>().transform.position;
            Vector2 spawnPosition = new Vector2(playerPosition.x, playerPosition.y + 1f);
            GameObject projectile = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
        }
    }

}

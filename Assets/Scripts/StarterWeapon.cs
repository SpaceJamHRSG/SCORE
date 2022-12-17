using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWeapon : Weapon {

    void Start() {
        baseAttackRate = 1;
        damageMultiplier = 1;

        StartCoroutine("WeaponAutoFire");
    }

    IEnumerator WeaponAutoFire() {

        while (true) {
            yield return new WaitForSeconds(1 / baseAttackRate);

            //print("Projectile fired | TODO: Instantiate projectile here!");
            //TODO: Instantiate Projectile
            //Instantiate();

        }

    }
}

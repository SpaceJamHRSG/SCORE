using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance == null) Debug.LogError("GameManager missing!");

            return instance;
        }
    }

    public GameObject playerPrefab;
    private GameObject playerReference;
    
    public PlayerStats playerStats;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerReference = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
    }



}

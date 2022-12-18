using Game;
using Music;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance == null) Debug.LogError("GameManager missing.");
            return instance;
        }
    }

    // Progress statistics
    private float survivalTime = 0; // Time spent alive, in seconds
    private int gruntsDefeated = 0; // Enemies killed
    //private int bossesDefeated = 0;

    public GameObject playerPrefab;
    public UpgradeSystem upgradeSystem;
    public RhythmManager rhythmManager;
    public GameObject gameOverScreen;

    private GameObject playerReference;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerReference = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        upgradeSystem.ActivePlayer = playerReference.GetComponent<PlayerManager>();
        rhythmManager.StartMainAudio();

    }

    private void Update() {
        survivalTime += Time.deltaTime;
    }

    public void EndGame() {
        // Game over
        Time.timeScale = 0.0f;

        int totalScore = (int)survivalTime * 10 + gruntsDefeated * 5;
        // TODO: leaderboard
        gameOverScreen.SetActive(true);
    }

    public void IncrementGruntsDefeated() {
        gruntsDefeated++;
    }

}

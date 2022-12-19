using System.Collections;
using Entity;
using Game;
using Music;
using UI;
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
    public int GruntsDefeated {
        get => gruntsDefeated;
        set => gruntsDefeated = value;
    }
    private int bossesDefeated = 0;
    public int BossesDefeated {
        get => bossesDefeated;
        set => bossesDefeated = value;
    }
    private int pointUpgrades = 0;
    public int PointUpgrades {
        get => pointUpgrades;
        set => pointUpgrades = value;
    }
    private int totalScore = 0;
    public int TotalScore {
        get => totalScore;
        set => totalScore = value;
    }

    public GameObject playerPrefab;
    public UpgradeSystem upgradeSystem;
    public RhythmManager rhythmManager;
    public GameObject gameOverScreen;
    public OverworldHUD HUD;

    private GameObject playerReference;
    private PlayerManager activePlayer;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        //Time.timeScale = 1.0f;

        playerReference = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        activePlayer = playerReference.GetComponent<PlayerManager>();
        upgradeSystem.ActivePlayer = activePlayer;
        StartCoroutine(StartAfter(0.5f));

        EnemyDirector.IsActive = true;
        EnemyGruntController.IsActive = true;
        activePlayer.IsActive = true;

        ExpLevelEntity.OnLevelUp += (i, e) =>
        {
            upgradeSystem.OpenUpgradeScreen(2);
            rhythmManager.FadeToRestAudio();
            EnemyGruntController.IsActive = false;
            activePlayer.IsActive = false;
            EnemyDirector.IsActive = false;
        };

        UpgradeSystem.OnClose += () =>
        {
            rhythmManager.FadeToMainAudio();
            EnemyGruntController.IsActive = true;
            activePlayer.IsActive = true;
            EnemyDirector.IsActive = true;
        };
    }

    private void Update() {
        survivalTime += Time.deltaTime;
        HUD.DisplayStats(activePlayer);
        totalScore = (int) survivalTime * 10 + gruntsDefeated * 5 + bossesDefeated * 50 + pointUpgrades * 500;

        foreach (var w in activePlayer.Weapons)
        {
            if (w.HasThisWeapon)
            {
                rhythmManager.MainAudio.UnmuteLine(w.LineName);
            }
            else
            {
                rhythmManager.MainAudio.MuteLine(w.LineName);
            }
        }
        
    }

    public void EndGame() {
        // Game over
        //Time.timeScale = 0.0f;

        // TODO: leaderboard
        gameOverScreen.SetActive(true);
    }

    IEnumerator StartAfter(float t)
    {
        yield return new WaitForSeconds(t);
        rhythmManager.StartMainAudio();
    }

}

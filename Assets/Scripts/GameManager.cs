using System;
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
    public Transform pauseScreen;

    private GameObject playerReference;
    private PlayerManager activePlayer;

    private bool _paused;
    private AudioSource _audio;

    public AudioSource Audio => _audio;

    public float OverallGameSpeed = 1.2f;

    private void Awake() {
        instance = this;
        _audio = GetComponent<AudioSource>();
    }

    private void Start() {
        Time.timeScale = OverallGameSpeed;

        playerReference = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        activePlayer = playerReference.GetComponent<PlayerManager>();
        upgradeSystem.ActivePlayer = activePlayer;
        StartCoroutine(StartAfter(0.5f));

        EnemyDirector.IsActive = true;
        EnemyGruntController.IsActive = true;
        activePlayer.IsActive = true;

        
    }

    private void OnEnable()
    {
        ExpLevelEntity.OnLevelUp += HandleLevelUp;
        UpgradeSystem.OnClose += HandleUpgradesClose;
    }

    private void OnDisable()
    {
        ExpLevelEntity.OnLevelUp -= HandleLevelUp;
        UpgradeSystem.OnClose -= HandleUpgradesClose;
    }

    private void HandleLevelUp(int i, ExpLevelEntity expLevelEntity)
    {
        Pause();
        upgradeSystem.OpenUpgradeScreen(2);
        rhythmManager.FadeToRestAudio();
        EnemyGruntController.IsActive = false;
        activePlayer.IsActive = false;
        EnemyDirector.IsActive = false;
    }

    private void HandleUpgradesClose()
    {
        Resume();
        rhythmManager.FadeToMainAudio();
        EnemyGruntController.IsActive = true;
        activePlayer.IsActive = true;
        EnemyDirector.IsActive = true;
    }
    private void Update() {
        survivalTime += Time.deltaTime;
        HUD.DisplayStats(activePlayer);
        totalScore = (int) (survivalTime * 20) + gruntsDefeated * 50 + bossesDefeated * 500 + pointUpgrades * 5000;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!_paused)
            {
                Pause();
                pauseScreen.gameObject.SetActive(true);
            }
            else
            {
                Resume();
                pauseScreen.gameObject.SetActive(false);
            }
        }

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

    public void Pause()
    {
        Time.timeScale = 0;
        rhythmManager.PauseAllLines();
        _paused = true;
    }

    public void Resume()
    {
        Time.timeScale = OverallGameSpeed;
        rhythmManager.ResumeAllLines();
        _paused = false;
    }

}

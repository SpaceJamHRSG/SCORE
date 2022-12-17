using Game;
using Music;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance == null) Debug.LogError("GameManager missing!");

            return instance;
        }
    }

    public GameObject playerPrefab;
    public UpgradeSystem upgradeSystem;
    public RhythmManager rhythmManager;
    private GameObject playerReference;
    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerReference = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        upgradeSystem.ActivePlayer = playerReference.GetComponent<PlayerManager>();
        rhythmManager.StartMainAudio();
    }


    public void EndGame() {
        // Game over
        Time.timeScale = 0.1f;
        SceneManager.LoadSceneAsync("GameOverUI", LoadSceneMode.Additive);
    }


}

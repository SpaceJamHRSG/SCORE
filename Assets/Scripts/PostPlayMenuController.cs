using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class PostPlayerMenuController : MonoBehaviour {

    public LootLockerLeaderboard leaderboard;
    private int leaderboardID = 9808;

    void Start()  {
        
    }

    void Update() {
    }

    public void SubmitAndReturn() {
        TMP_InputField inputField = FindObjectOfType<TMP_InputField>();
        if (inputField.text != "") PlayerPrefs.SetString("PlayerID", inputField.text);
        StartCoroutine(LoginAndSubmitScoreRoutine());
    }

    IEnumerator LoginAndSubmitScoreRoutine() {

        // Login
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) => {
            if (response.success) {
                Debug.Log("Logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            } else {
                Debug.Log("Cound not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

        // Submit score
        done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, GameManager.Instance.TotalScore, leaderboardID, (response) => {
            if (response.success) {
                Debug.Log("Score submitted");
                done = true;
            } else {
                Debug.Log("Failed submit" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

        LoadMainMenu();
    }

    public void LoadMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
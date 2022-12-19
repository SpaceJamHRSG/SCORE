using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;


public class PostPlayMenuController : MonoBehaviour {

    private int leaderboardID = 9808; // stage
    //private int leaderboardID = 9833; // live
    int scoreCount = 10;

    [SerializeField] private TMP_Text showScoreText;

    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    public TMP_InputField playerNameInputField;

    void Start()  {

        StartCoroutine(FetchHighscores());

    }

    void Update() {
        showScoreText.text = GameManager.Instance.TotalScore.ToString();
    }

    IEnumerator FetchHighscores() {

        // Login
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) => {
            if (response.success) {
                //Debug.Log("Logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            } else {
                //Debug.Log("Cound not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

        // Get Scores
        done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (response) => {
            if (response.success) {

                string tempPlayerNames = "Name\n";
                string tempPlayerScores = "Score\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++) {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "") {
                        tempPlayerNames += members[i].player.name;
                    } else {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;

                // Display scores
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
                //Debug.Log("names:" + tempPlayerNames);
                //Debug.Log("scores:" + tempPlayerScores);

            } else {
                //Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

        playerNameInputField.text = PlayerPrefs.GetString("PlayerID");
    }

    public void SubmitAndReturn() {
        StartCoroutine(SubmitAndQuit());
    }
    IEnumerator SubmitAndQuit() {

        string name = playerNameInputField.text;
        name = name.Substring(0, 8); // char limit

        LootLockerSDKManager.SetPlayerName(name, (response) => {
            if (response.success) {
                //Debug.Log("Name set");
            } else {
                //Debug.Log("Failed name set" + response.Error);
            }
        });

        // Submit score
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID"); 
        LootLockerSDKManager.SubmitScore(playerID, GameManager.Instance.TotalScore, leaderboardID, (response) => {
            if (response.statusCode == 200) {
                //Debug.Log("Score submitted");
                //Debug.Log(playerID + ":" + GameManager.Instance.TotalScore);
                done = true;
            } else {
                //Debug.Log("Failed submit" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

        LoadMainMenu();
    }

    public void LoadMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    /*
    IEnumerator LoginRoutine() {

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
    }

    IEnumerator SubmitScoreRoutine() {

        // Submit score
        bool done = false;
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
    }

    public IEnumerator GetScoresRoutine() {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (response) => {
            if (response.success) {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++) {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "") {
                        tempPlayerNames += members[i].player.name;
                    } else {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;

            } else {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    */
    
}

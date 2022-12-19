using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class MainMenuLeaderboard : MonoBehaviour {

    private int leaderboardID = 9808; // stage
    //private int leaderboardID = 9833; // live

    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        StartCoroutine(FetchHighscores());
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
    }

}

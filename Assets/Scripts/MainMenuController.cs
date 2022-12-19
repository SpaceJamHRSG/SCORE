using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
        
    }


    public void PlayGame() {
        SceneManager.LoadScene("PlayScene");
    }

    public void ShowLeaderboard() {

    }

    public void ShowCredits() {

    }
}

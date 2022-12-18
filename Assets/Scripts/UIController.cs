using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame() {
        SceneManager.LoadSceneAsync("PostPlayMenu");
    }

    public void LoadMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartupScreen : MonoBehaviour {

    public TMP_Text text; // Text to fade
    public float fadeDuration;
    private float currentTime;
    private bool isKeyPressed;

    // Start is called before the first frame update
    void Start() {
        isKeyPressed = false;
        Debug.Assert(text);
    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;

        if (Input.anyKeyDown) {
            isKeyPressed = true;
        }

        if (isKeyPressed) {
            FadeOutText();
            Invoke("ChangeScene", fadeDuration);
        }
    }

    void ChangeScene() {
        SceneManager.LoadScene("MainMenu");
    }

    void FadeOutText() {
        // Calculate the alpha
        float alpha = 1 - (currentTime / fadeDuration);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

    }
}

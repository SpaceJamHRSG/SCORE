using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartupScreen : MonoBehaviour {

    public Image fadingImage;
    public float fadeDuration;
    private float currentTime;
    private bool isKeyPressed;
    private bool isChanging;

    // Start is called before the first frame update
    void Start() {
        isKeyPressed = false;
        isChanging = false;
        Debug.Assert(fadingImage);
    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;

        if (Input.anyKeyDown) {
            isKeyPressed = true;
            currentTime = 0;
        }

        if (isKeyPressed) {
            FadeOutImage();
            if (!isChanging) {
                Invoke("ChangeScene", fadeDuration);
                isChanging = true;
            }
        }
    }

    void ChangeScene() {
        SceneManager.LoadScene("MainMenu");
    }

    void FadeOutImage() {
        float alpha = 1 - (currentTime / fadeDuration);
        print(alpha);
        fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.g, fadingImage.color.b, alpha);
    }

    /*
    using TMPro;

    public TMP_Text text; // Text to fade
    void FadeOutText() {
        // Calculate the alpha
        float alpha = 1 - (currentTime / fadeDuration);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

    }

    */
}

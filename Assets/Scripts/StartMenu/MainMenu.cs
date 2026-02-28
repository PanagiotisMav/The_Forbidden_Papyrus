using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;   // Duration of fade
    private bool isFading = false;
    private float fadeTimer = 0f;
    private int targetSceneIndex = 1;

    void Start()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f; // Start transparent
            fadeImage.color = color;
        }
    }

    void Update()
    {
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;

            if (fadeTimer >= fadeDuration)
            {
                SceneManager.LoadScene(targetSceneIndex);
            }
        }
    }

    public void PlayGame()
    {
        if (fadeImage != null)
        {
            isFading = true;
            fadeTimer = 0f;
        }
        else
        {
            // Fallback if fadeImage is missing
            SceneManager.LoadScene(targetSceneIndex);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

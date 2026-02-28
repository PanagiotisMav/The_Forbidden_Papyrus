using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ManageButtons : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 3f;

    public void RestartGame()
    {
        StartCoroutine(FadeOutAndRestart());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeOutAndRestart()
    {
        float t = 0;
        Color originalColor = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, normalizedTime);
            yield return null;
        }

        // Add scene loaded callback before reloading
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Play ambient music when scene is loaded
        if (MusicManager.Instance != null && MusicManager.Instance.ambientMusic != null)
        {
            MusicManager.Instance.ambientMusic.Play();
        }

        // Unsubscribe to avoid multiple triggers
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

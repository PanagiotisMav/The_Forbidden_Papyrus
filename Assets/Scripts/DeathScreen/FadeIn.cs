using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float fadeDuration = 2f; // How long the fade takes
    private Image fadeImage;
    private float fadeTimer = 0f;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        Color color = fadeImage.color;
        color.a = 1f;// Start fully black
        fadeImage.color = color;
    }

    void Update()
    {
        if (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

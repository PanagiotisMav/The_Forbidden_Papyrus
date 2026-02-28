using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource ambientMusic;
    public AudioSource chaseMusic;
    public AudioSource skeletonMusic;

    private int activeChasers = 0;
    private Coroutine fadeOutCoroutine;
    private Coroutine skeletonFadeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Preload clips
        if (ambientMusic != null)
        {
            ambientMusic.volume = 0f;
            ambientMusic.Play();
            ambientMusic.Stop();
            ambientMusic.volume = 1f;
        }

        if (chaseMusic != null)
        {
            chaseMusic.volume = 0f;
            chaseMusic.Play();
            chaseMusic.Stop();
            chaseMusic.volume = 1f;
        }

        if (skeletonMusic != null)
        {
            skeletonMusic.volume = 0f;
            skeletonMusic.Play();
            skeletonMusic.Stop();
            skeletonMusic.volume = 1f;
        }

        if (!ambientMusic.isPlaying)
        {
            ambientMusic.Play();
        }

    }

    public void StartChase()
    {
        activeChasers++;

        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
            chaseMusic.volume = 1f;
        }

        if (!chaseMusic.isPlaying)
            chaseMusic.Play();

        if (ambientMusic.isPlaying)
            ambientMusic.Stop();
    }

    public void StopChase()
    {
        activeChasers = Mathf.Max(0, activeChasers - 1);

        if (activeChasers == 0)
        {
            if (fadeOutCoroutine != null)
                StopCoroutine(fadeOutCoroutine);

            fadeOutCoroutine = StartCoroutine(FadeOutAudio(chaseMusic, 1.5f, () => {
                if (!ambientMusic.isPlaying)
                    ambientMusic.Play();
            }));
        }
    }

    public void PlaySkeletonMusic()
    {
        if (skeletonFadeCoroutine != null)
            StopCoroutine(skeletonFadeCoroutine);

        if (chaseMusic.isPlaying)
            chaseMusic.Stop();
        if (ambientMusic.isPlaying)
            ambientMusic.Stop();

        skeletonFadeCoroutine = StartCoroutine(FadeInAudio(skeletonMusic, 1.5f));
    }

    public void StopSkeletonMusic()
    {
        if (skeletonFadeCoroutine != null)
            StopCoroutine(skeletonFadeCoroutine);

        skeletonFadeCoroutine = StartCoroutine(FadeOutAudio(skeletonMusic, 1.5f, () => {
            if (!ambientMusic.isPlaying)
                ambientMusic.Play();
        }));
    }

    // Fades in the given AudioSource over 'duration' seconds
    private IEnumerator FadeInAudio(AudioSource audioSource, float duration)
    {
        if (audioSource == null)
            yield break;

        audioSource.volume = 0f;
        if (!audioSource.isPlaying)
            audioSource.Play();

        float time = 0f;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f;
    }

    // Fades out the given AudioSource over 'duration' seconds, then optionally calls onComplete
    private IEnumerator FadeOutAudio(AudioSource audioSource, float duration, System.Action onComplete = null)
    {
        if (audioSource == null)
            yield break;

        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;

        onComplete?.Invoke();
    }

    public void StopAllMusicAndPlayAmbient()
    {
        // Stop all music
        if (chaseMusic != null && chaseMusic.isPlaying)
            chaseMusic.Stop();

        if (skeletonMusic != null && skeletonMusic.isPlaying)
            skeletonMusic.Stop();

        if (ambientMusic != null)
        {
            if (ambientMusic.isPlaying)
                ambientMusic.Stop();

            StartCoroutine(FadeInAudio(ambientMusic, 1.5f));
        }

        activeChasers = 0;
    }

}

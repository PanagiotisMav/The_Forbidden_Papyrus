using UnityEngine;

public class SkeletonMusicTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.Instance?.PlaySkeletonMusic();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.Instance?.StopSkeletonMusic();
        }
    }
}

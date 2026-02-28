using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MegaPack : MonoBehaviour
{
    public RectTransform imageRect;
    public Inventory playerInventory;
    public GameObject pauseMenuUI;


    private bool isOpen = false;
    private bool isPaused = false;

    private void Start()
    {
        imageRect.localScale = Vector3.zero;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerInventory.HasItem("Scroll"))
            {
                ToggleImage();
            }
            else
            {
                Debug.Log("You need the Scroll to open this.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void ToggleImage()
    {
        if (isOpen)
        {
            imageRect.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
        }
        else
        {
            imageRect.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
        }

        isOpen = !isOpen;
    }

public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        AudioListener.pause = isPaused;

        // Show or hide the cursor based on pause state
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Game is resuming — refresh sensitivity
            CameraManager cameraManager = FindObjectOfType<CameraManager>();
            if (cameraManager != null)
            {
                cameraManager.RefreshSensitivity();
            }
        }
    }
    public void ExitGame()
    {
        // Reset pause states
        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Stop all current music
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.StopAllMusicAndPlayAmbient();
        }

        // Load main menu
        SceneManager.LoadScene(0);
    }

}

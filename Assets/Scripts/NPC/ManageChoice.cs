using UnityEngine;
using UnityEngine.UI;

public class ManageChoice : MonoBehaviour
{
    public Button[] buttons;


    void Start()
    {
        HideAllButtons();
    }

    public void HideAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

}

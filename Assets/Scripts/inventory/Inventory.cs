using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image keySlot;
    public Image scrollSlot;
    public Image diplomaSlot;

    public Sprite emptySprite;
    public Sprite keySprite;
    public Sprite scrollSprite;
    public Sprite diplomaSprite;

    private bool hasKey = false;
    private bool hasScroll = false;
    private bool hasDiploma = false;
    

    public void AddItem(string itemName)
    {
        switch (itemName)
        {
            case "Key":
                hasKey = true;
                keySlot.sprite = keySprite;
                break;
            case "Scroll":
                hasScroll = true;
                scrollSlot.sprite = scrollSprite;
                break;
            case "Diploma":
                hasDiploma = true;
                diplomaSlot.sprite = diplomaSprite;
                break;
        }
    }

    public bool HasItem(string itemName)
    {
        return itemName switch
        {
            "Key" => hasKey,
            "Scroll" => hasScroll,
            "Diploma" => hasDiploma,
            _ => false,
        };
    }
}

using UnityEngine;
using System.Collections;
using TMPro;

public class CollectibleCount : MonoBehaviour
{
    
    public TMPro.TMP_Text text;
    

    int count;

    
    void OnEnable()
    {
        Collectible.OnCollected += OnCollectibleCollected;
    }

    void OnDisable()
    {
        Collectible.OnCollected -= OnCollectibleCollected;
    }

    void OnCollectibleCollected(){
        count = count + 1;
        text.text = (count).ToString() + "/4 collectibles found";
    }

    public int GetCount()
    {
        return count;
    }

    public void Subtract(int amount)
    {
        count -= amount;
        if (count < 0) count = 0;
        text.text = count.ToString() + "/4 collectibles found";

        
        StopAllCoroutines();
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        // Save original values
        Color originalColor = text.color;
        FontStyles originalStyle = text.fontStyle;

        // Apply red
        text.color = Color.red;

        yield return new WaitForSeconds(1f);

        // Original style
        text.color = originalColor;
        text.fontStyle = originalStyle;
    }


}

using UnityEngine;

public class AIHitController : MonoBehaviour
{
    public BatHitbox batHitbox;

    public void EnableBatDamage()
    {
        if (batHitbox != null)
        {
            batHitbox.EnableDamage();
        }
    }

    public void DisableBatDamage()
    {
        if (batHitbox != null)
        {
            batHitbox.DisableDamage();
        }
    }
}

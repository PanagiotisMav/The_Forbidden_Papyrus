using UnityEngine;

public class AttachGunToHand : MonoBehaviour
{
    public Animator animator;
    public GameObject gunPrefab;
    private GameObject gunInstance;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        Transform rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);

        if (rightHand != null)
        {
            gunInstance = Instantiate(gunPrefab, rightHand);

            gunInstance.transform.localPosition = new Vector3(0.027f, 0.1553f, -0.0339f);
            gunInstance.transform.localRotation = Quaternion.Euler(97.5230f, 187.177f, 96.838f);

            gunInstance.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogError("Right Hand bone not found!");
        }
    }
}

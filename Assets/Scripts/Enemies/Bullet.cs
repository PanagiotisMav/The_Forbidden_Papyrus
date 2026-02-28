using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 10f;

    void Start()
    {
        // Destroy after 10 sec
        Destroy(gameObject, lifetime);

        // Set up the trail
        TrailRenderer trail = GetComponent<TrailRenderer>();
        trail.time = 0.3f;
        trail.startWidth = 0.02f;
        trail.endWidth = 0f;
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = Color.yellow;
        trail.endColor = new Color(1, 1, 0, 0);
    }

    void Update()
    {
        transform.position += -transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(40f);
            }
        }

        Destroy(gameObject);
    }

}

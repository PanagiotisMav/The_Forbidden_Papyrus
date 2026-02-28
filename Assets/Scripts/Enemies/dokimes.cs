using UnityEngine;
using UnityEngine.AI;

public class dokimes : MonoBehaviour
{
    NavMeshAgent myAgent;
    Animator animator;

    public Transform myTarget;
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    public AudioClip shootSound;
    public AudioClip movingSound;
    private AudioSource shootingSound;
    private AudioSource walkingSound;

    public int chaseRange;
    public int shootRange;
    private Vector3 startingPosition;

    private float shootCooldown = 3f;
    private float shootTimer = 0f;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        shootingSound = audioSources[0];
        walkingSound = audioSources[1];
        startingPosition = transform.position;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, myTarget.position);

        if (dist < chaseRange)
        {
            myAgent.destination = myTarget.position;
        }
        else
        {
            myAgent.destination = startingPosition;
        }

        bool isMoving = myAgent.velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isMoving);
        bool isInShootRange = dist < shootRange;

        if (!isMoving && isInShootRange)
        {
            animator.SetBool("isIdleShooting", true);
            
        }
        else
        {
            animator.SetBool("isIdleShooting", false);
        }

        HandleMovingSound(isMoving);

        if (isInShootRange)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootCooldown)
            {
                Shoot();
                shootTimer = 0f;
            }

            animator.SetBool("isShooting", isMoving);
        }
        else
        {
            shootTimer = 0f;
            animator.SetBool("isShooting", false);
        }
    }


    void Shoot()
    {
        animator.SetTrigger("Aishoot");

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (shootSound != null && shootingSound != null)
        {
            shootingSound.PlayOneShot(shootSound);
        }
    }

    void HandleMovingSound(bool isMoving)
    {
        if (isMoving)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.clip = movingSound;
                walkingSound.loop = true;
                walkingSound.Play();
            }
        }
        else
        {
            if (walkingSound.clip == movingSound)
            {
                walkingSound.Stop();
            }
        }
    }
}

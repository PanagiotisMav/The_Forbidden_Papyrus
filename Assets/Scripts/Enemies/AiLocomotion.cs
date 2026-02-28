using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    NavMeshAgent myAgent;
    Animator animator;

    public Transform myTarget;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private PlayerStats playerStats;

    /**/
    private bool isChasing = false;



    public AudioClip shootSound;
    public AudioClip movingSound;
    private AudioSource shootingSound;
    private AudioSource walkingSound;

    public int chaseRange;
    public int shootRange;
    private Vector3 startingPosition;

    private float shootCooldown = 2f;
    private float shootTimer = 0f;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerStats = myTarget.GetComponent<PlayerStats>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        shootingSound = audioSources[0];
        walkingSound = audioSources[1];
        startingPosition = transform.position;
    }

    void Update()
    {
        if (playerStats != null && playerStats.IsDead())
        {
            myAgent.destination = transform.position;
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdleShooting", false);
            animator.SetBool("isShooting", false);

            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
            if (shootingSound.isPlaying)
            {
                shootingSound.Stop();
            }
            
            return;
        }


        float dist = Vector3.Distance(transform.position, myTarget.position);

    if (dist < chaseRange)
    {
        myAgent.destination = myTarget.position;

        if (!isChasing)
        {
            isChasing = true;
            MusicManager.Instance.StartChase();
        }
    }
    else
    {
        myAgent.destination = startingPosition;

        if (isChasing)
        {
            isChasing = false;
            MusicManager.Instance.StopChase();
        }
    }

        bool isMoving = myAgent.velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isMoving);
        bool isInShootRange = dist < shootRange;

        if (!isMoving && isInShootRange)
        {
            animator.SetBool("isIdleShooting", true);

            //AI Look at player
            Vector3 direction = (myTarget.position - transform.position).normalized;
            direction.y = 0f;// prevent tilting up/down
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
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

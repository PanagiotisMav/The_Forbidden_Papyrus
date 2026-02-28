using UnityEngine;
using UnityEngine.AI;

public class Ai2Locomotion : MonoBehaviour
{
    NavMeshAgent myAgent;
    Animator animator;

    public Transform myTarget;

    private PlayerStats playerStats;

    public AudioClip movingSound;
    private AudioSource walkingSound;

    public int chaseRange = 10;
    public float attackRange = 2f;
    private Vector3 startingPosition;

    private float attackCooldown = 2f;
    private float attackTimer = 0f;

    private bool isChasing = false;



    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerStats = myTarget.GetComponent<PlayerStats>();

        walkingSound = GetComponent<AudioSource>();
        startingPosition = transform.position;
    }


    void AttackPlayer()
    {
        FaceTarget();

        if (attackTimer >= attackCooldown)
        {
            animator.SetTrigger("Hit"); 
            attackTimer = 0f;
        }
    }

    void Update()
    {
        if (playerStats != null && playerStats.IsDead())
        {
            myAgent.isStopped = true;
            animator.SetBool("isRunning", false);

            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }

            return;
        }

        float dist = Vector3.Distance(transform.position, myTarget.position);
        attackTimer += Time.deltaTime;

        bool isInAttackRange = dist <= attackRange;
        bool isInChaseRange = dist < chaseRange && dist > attackRange;

        
        if (isInChaseRange || isInAttackRange)
        {
            if (!isChasing)
            {
                isChasing = true;
                MusicManager.Instance.StartChase();
            }
        }
        else
        {
            if (isChasing)
        {
            isChasing = false;
            MusicManager.Instance.StopChase();
        }
        }

        
        if (isInChaseRange)
        {
            myAgent.isStopped = false;
            myAgent.destination = myTarget.position;
        }
        else if (isInAttackRange)
        {
            myAgent.isStopped = true;
            AttackPlayer();
        }
        else
        {
            myAgent.isStopped = false;
            myAgent.destination = startingPosition;
        }

        bool isMoving = myAgent.velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isMoving);

        if (!isMoving && dist < chaseRange)
        {
            FaceTarget();
        }

        HandleMovingSound(isMoving);
    }


    void FaceTarget()
    {
        Vector3 direction = (myTarget.position - transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }
    }

}

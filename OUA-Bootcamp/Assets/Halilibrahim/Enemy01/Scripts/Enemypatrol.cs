using UnityEngine;
using UnityEngine.AI;

public class Enemypatrol : MonoBehaviour
{
    public Transform target;
    public float minChaseSpeed = 3f;
    public float maxChaseSpeed = 5f;
    public float delayBeforeMovement = 1.3f;
    public float patrolStopDuration = 1.3f;

    private Animator animator;
    private NavMeshAgent agent;
    private bool isDead = false;
    private bool canMove = true;
    private bool isPatrolling = true;
    private float currentSpeed;
    private float timeSinceStop = 0f;

    private float timeSinceHit = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GetRandomSpeed();
        currentSpeed = agent.speed;
    }

    void Update()
    {
        if (!isDead)
        {
            if (canMove)
            {
                if (animator.GetInteger("hit") != 0)
                {
                    StopMovement();
                    isPatrolling = false;
                    timeSinceHit = Time.time;
                }
                else if (!isPatrolling && Time.time - timeSinceHit >= patrolStopDuration)
                {
                    ResumePatrolling();
                }

                if (isPatrolling)
                {
                    ChaseTarget();
                    UpdateAnimation();
                }
            }
            else if (isPatrolling && Time.time - timeSinceStop >= delayBeforeMovement)
            {
                ResumePatrolling();
            }
        }
        else
        {
            StopMovement();
        }
    }

    void ChaseTarget()
    {
        if (target != null && !animator.GetBool("death"))
        {
            agent.SetDestination(target.position);
        }
    }

    void UpdateAnimation()
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    void StopMovement()
    {
        agent.isStopped = true;
        float speed = 0f;
        animator.SetFloat("Speed", speed);
    }

    void ResumePatrolling()
    {
        canMove = true;
        agent.speed = currentSpeed;
        isPatrolling = true;
        agent.isStopped = false; // Devriye yeniden baþladýðýnda harekete devam et
    }

    float GetRandomSpeed()
    {
        return Random.Range(minChaseSpeed, maxChaseSpeed);
    }

    public void SetDead(bool value)
    {
        isDead = value;
        animator.SetBool("death", isDead);
        if (isDead)
        {
            StopMovement();
        }
        else
        {
            timeSinceStop = Time.time;
            canMove = false;
            agent.speed = 0f;
            agent.isStopped = true; // Devriye durduðunda hareketi duraklat
        }
    }
}

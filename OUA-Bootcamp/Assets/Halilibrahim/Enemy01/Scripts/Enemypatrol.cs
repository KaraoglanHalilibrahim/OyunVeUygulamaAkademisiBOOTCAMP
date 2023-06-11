using UnityEngine;
using UnityEngine.AI;

public class Enemypatrol : MonoBehaviour
{
    public Transform target;
    public float minChaseSpeed = 3f;
    public float maxChaseSpeed = 5f;
    public float delayBeforeMovement = 1.5f;

    private Animator animator;
    private NavMeshAgent agent;
    private bool isDead = false;
    private bool canMove = false;

    private float currentSpeed;
    private float timeSinceStop = 0f;

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
                ChaseTarget();
                UpdateAnimation();
            }
            else
            {
                timeSinceStop += Time.deltaTime;
                if (timeSinceStop >= delayBeforeMovement)
                {
                    canMove = true;
                    agent.speed = currentSpeed;
                }
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
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
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
            timeSinceStop = 0f;
            canMove = false;
            agent.speed = 0f;
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class Enemypatrol : MonoBehaviour
{
    public Transform target;
    public float minChaseSpeed = 3f;
    public float maxChaseSpeed = 5f;

    private Animator animator;
    private NavMeshAgent agent;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GetRandomSpeed();
    }

    void Update()
    {
        if (!isDead)
        {
            ChaseTarget();
            UpdateAnimation();
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
    }
}

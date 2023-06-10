using UnityEngine;
using UnityEngine.AI;

public class Enemypatrol : MonoBehaviour
{
    public Transform target;
    public float minChaseSpeed = 3f;
    public float maxChaseSpeed = 5f;

    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GetRandomSpeed();
    }

    void Update()
    {
        ChaseTarget();
        UpdateAnimation();
    }

    void ChaseTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void UpdateAnimation()
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    float GetRandomSpeed()
    {
        return Random.Range(minChaseSpeed, maxChaseSpeed);
    }
}

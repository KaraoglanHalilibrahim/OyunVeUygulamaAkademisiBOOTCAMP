using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            animator.SetBool("attack", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            animator.SetBool("attack", false);
        }
    }
}

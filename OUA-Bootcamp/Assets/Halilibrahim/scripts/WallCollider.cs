using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public Animator animator;
    private bool LeftWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            LeftWall = true;
            animator.SetBool("LeftWall", LeftWall);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            LeftWall = false;
            animator.SetBool("LeftWall", LeftWall);
        }
    }
}

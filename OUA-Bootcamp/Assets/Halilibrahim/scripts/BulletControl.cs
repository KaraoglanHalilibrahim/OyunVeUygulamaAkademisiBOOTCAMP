using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public GameObject targetObject;
    public Animator targetAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            targetAnimator.SetBool("active", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            targetAnimator.SetBool("active", false);
        }
    }
}

using UnityEngine;

public class death_enemy : MonoBehaviour
{
    public Animator animator;
    public Rigidbody characterRigidbody;

    private bool isDead = false;

    private void Update()
    {
        if (isDead)
        {
            // Karakterin bütün hareketlerini durdur
            characterRigidbody.velocity = Vector3.zero;
            characterRigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void SetDead(bool value)
    {
        isDead = value;
        animator.SetBool("death", value);
    }
}

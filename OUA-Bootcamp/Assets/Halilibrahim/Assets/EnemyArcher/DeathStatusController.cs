using UnityEngine;

public class DeathStatusController : MonoBehaviour
{
    public Animator animator;
    private bool hasRandomizedStatus = false;

    private void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // "death" parametresi true olduðunda ve henüz rastgele deðer alýnmamýþsa
        if (animator.GetBool("death") && !hasRandomizedStatus)
        {
            // "DeathStatu" parametresine rastgele bir deðer ata
            int randomStatus = Random.Range(1, 4);
            animator.SetInteger("DeathStatu", randomStatus);
            hasRandomizedStatus = true;
        }
    }
}

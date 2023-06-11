using UnityEngine;

public class enemy_death : MonoBehaviour
{
    public Animator animator; // Animator bileþeni referansý
    private bool isDead = false; // Ölüm durumu

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Çarpýþan obje "Bullet" tag'ine sahipse
        {
            isDead = true; // Ölüm durumunu true yap
            animator.SetBool("death", isDead); // Animator'deki "death" parametresine ölüm durumunu atar
        }
    }
}

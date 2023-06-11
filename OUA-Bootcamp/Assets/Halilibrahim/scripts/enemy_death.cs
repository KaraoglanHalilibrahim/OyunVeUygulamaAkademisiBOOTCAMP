using UnityEngine;

public class enemy_death : MonoBehaviour
{
    public Animator animator; // Animator bileþeni referansý
    public bool death = false; // Ölüm durumu

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Çarpýþma objesi "Bullet" tag'ine sahipse
        {
            death = true; // Ölüm durumunu true yap
            animator.SetBool("death", death); // Animator'deki "death" parametresine ölüm durumunu atar
        }
    }
}

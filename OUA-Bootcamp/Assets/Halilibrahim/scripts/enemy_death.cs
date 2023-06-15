using UnityEngine;

public class enemy_death : MonoBehaviour
{
    public Animator animator; // Animator bileþeni referansý
    public CapsuleCollider capsuleCollider; // CapsuleCollider bileþeni referansý
    private bool isDead = false; // Ölüm durumu
    private int hit = 0; // Hit deðeri
    private int health = 100; // Saðlýk deðeri
    private bool isFrozen = false; // Hareketin durma durumu
    private float freezeDuration = 1f; // Hareketin durma süresi
    private float freezeTimer = 0f; // Hareketin durma süresi için zamanlayýcý

    private void Start()
    {
    }

    private void Update()
    {
        if (isFrozen)
        {
            // Hareketi durdurma süresini kontrol et
            freezeTimer += Time.deltaTime;
            if (freezeTimer >= freezeDuration)
            {
                isFrozen = false;
                freezeTimer = 0f;

                // Hareketin tekrar baþlamasýný saðla
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Çarpýþan obje "Bullet" tag'ine sahipse
        {
            animator.SetBool("death", isDead); // Animator'deki "death" parametresine ölüm durumunu atar

            // Rastgele hit deðeri
            hit = Random.Range(1, 3);
            animator.SetInteger("hit", hit);

            // Saðlýk deðerinden rastgele düþüþ
            int damage = Random.Range(40, 51);
            health -= damage;

            if (health <= 0)
            {
                health = 0;
                isDead = true;
            }

          
            // Hit deðerini sýfýrlama
            hit = 0;
            Invoke(nameof(ResetHit), 1f);
        }
    }

    private void ResetHit()
    {
        animator.SetInteger("hit", 0);
    }
}

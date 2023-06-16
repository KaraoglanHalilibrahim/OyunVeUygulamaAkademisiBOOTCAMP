using UnityEngine;

public class enemy_death : MonoBehaviour
{
    public Animator animator; // Animator bileþeni referansý
    public CapsuleCollider capsuleCollider; // CapsuleCollider bileþeni referansý
    public GameObject enemySphere; // EnemySphere objesinin referansý
    private bool isDead = false; // Ölüm durumu
    private int hit = 0; // Hit deðeri
    [SerializeField] public int health = 100; // Saðlýk deðeri
    private bool isFrozen = false; // Hareketin durma durumu
    private float freezeDuration = 0f; // Hareketin durma süresi
    private float freezeTimer = 0f; // Hareketin durma süresi için zamanlayýcý


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

        if (isDead)
        {
            // Ölüm sonrasý 3 saniye bekledikten sonra karakteri sahneden sil
            Invoke(nameof(DestroyCharacter), 3f);
        }
    }

    private void DestroyCharacter()
    {
        // Karakteri sahneden sil
        Destroy(gameObject);
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

            if (health <= 0 && !isDead)
            {
                health = 0;
                isDead = true;
                // Ölüm durumuna geçiþ iþlemleri burada yapýlabilir
                animator.SetBool("death", isDead); // Animator'deki "death" parametresine ölüm durumunu atar

                // EnemySphere objesini devre dýþý býrak
                if (enemySphere != null)
                {
                    enemySphere.SetActive(false);
                }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            
                animator.SetBool("punching?", true); // Animator'deki "punching?" parametresini true yap
                
                if(animator.GetBool("punching?") == true)
            {
                health -= 7; // punching? true olduðunda health deðerinden  azalt
                animator.SetBool("punching?", false); // Animator'deki "punching?" parametresini false yap

            }

            if (health <= 0 && !isDead)
            {
                health = 0;
                isDead = true;
                animator.SetBool("death", isDead); // Animator'deki "death" parametresine ölüm durumunu atar
                                                   // Ölüm durumuna geçiþ iþlemleri burada yapýlabilir
                if (enemySphere != null)
                {
                    enemySphere.SetActive(false);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            animator.SetBool("punching?", false); // Animator'deki "punching?" parametresini false yap

        }
    }
}

using UnityEngine;

public class CharacterExplosion : MonoBehaviour
{
    public Animator animator; // Karakterin animatör bileþeni
    public GameObject bodyPartPrefab; // Parça nesnesi prefab'i
    public int numBodyParts = 10; // Oluþturulacak parça nesnelerinin sayýsý
    public float explosionForce = 10f; // Patlama kuvveti
    public float explosionRadius = 5f; // Patlama yarýçapý

    private bool isDead = false; // Ölüm durumu kontrolü

    public void Update()
    {
        // Ölüm animasyonunun oynatýldýðý durumu kontrol et
        if (animator.GetBool("death") && !isDead)
        {
            isDead = true;
            Explode();
        }
    }

    private void Explode()
    {
        // Karakterin boyutunu küçült
        transform.localScale = Vector3.one * 0.1f;

        // Parça nesnelerini oluþtur ve patlama kuvveti uygula
        for (int i = 0; i < numBodyParts; i++)
        {
            GameObject bodyPart = Instantiate(bodyPartPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bodyPart.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = bodyPart.AddComponent<Rigidbody>(); // Rigid body bileþeni ekle
            }

            // Rastgele bir patlama kuvveti uygula
            Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rb.AddForce(randomForce * explosionForce, ForceMode.Impulse);
        }

        // Karakteri yok et
        Destroy(gameObject);
    }
}

using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public Transform characterTransform;
    public float rotationSpeed = 5f; // Dönüþ hýzýný kontrol etmek için bir deðer

    private bool attack = false;
    private bool isDead = false;
    private int attackValue = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            attack = true;
            attackValue = Random.Range(1, 6);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            attack = false;
        }
    }

    private void Update()
    {
        animator.SetBool("attack", attack);
        animator.SetInteger("attack?", attackValue);

        if (!isDead && attack)
        {
            // Karakterin karakter objesine doðru bakmasýný saðlar
            Vector3 direction = characterTransform.position - transform.position;
            direction.y = 0f; // Sadece yatay düzlemde dönmesini saðlar
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void SetDead(bool value)
    {
        isDead = value;
    }
}

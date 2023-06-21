using UnityEngine;

public class BoyTimer : MonoBehaviour
{
    public Animator animator;
    private bool dodge = false;
    private float timer = 0f;
    private bool gameStarted = false;

    private void Update()
    {
        if (!gameStarted)
        {
            timer += Time.deltaTime;
            if (timer >= 10f)
            {
                dodge = true;
                
                animator.SetBool("dodge", dodge); // Animatordeki "dodge" parametresini güncelle
            }
            if (timer >= 17f) // Oyun baþladýktan 17 saniye sonra
            {
                dodge = false;
                animator.SetBool("dodge", dodge); // Animatordeki "dodge" parametresini güncelle
                gameStarted = true;
                transform.position += new Vector3(0f, 2.75f, 0f); // Karakteri y ekseninde 5 birim yukarýya taþý
                transform.rotation = Quaternion.Euler(0f, 80f, 0f);

            }
        }
    }
}

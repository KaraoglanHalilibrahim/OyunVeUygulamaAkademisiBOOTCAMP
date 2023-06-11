using UnityEngine;

public class walk_Gun : MonoBehaviour
{
    private Animator animator;
    private bool walk = false;

    private void Start()
    {
        // Karakterinizdeki Animator bileþenini alýn
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // WASD tuþlarýnýn kullanýmýný kontrol et
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // Yürüme boolean'ýný true yap
            walk = true;
        }
        else
        {
            // Yürüme boolean'ýný false yap
            walk = false;
        }

        // Animator'deki "walk" parametresini "walk" boolean'ýna eþitle
        animator.SetBool("walk", walk);
    }
}

using System.Collections;
using UnityEngine;

public class ArcherShoot : MonoBehaviour
{
    public GameObject arrowPrefab; // Ok prefab'ý
    public Transform arrowSpawnPoint; // Okun çýkacaðý nokta
    public float initialDelay = 2f; // Ýlk atýþ için geçmesi gereken süre (saniye)
    public float shootInterval = 1.5f; // Ardýþýk atýþlar arasýndaki zaman farký (saniye)
    public float force = 1000f; // Okun hýz ve gücü

    private bool canShoot = true;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        
            yield return new WaitForSeconds(initialDelay);


        while (true)
        {
            if (canShoot && animator.GetBool("attack"))
            {
                Shoot();
                canShoot = false;
                yield return new WaitForSeconds(shootInterval);
                canShoot = true;
            }
            yield return null;
        }
    }

    private void Shoot()
    {
        
        
             // Ok objesini oluþtur
              GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

             // Oku hareket ettirme veya diðer ayarlarý yapma kodlarý eklenebilir

             // Örneðin, Rigidbody bileþeni varsa hareket ettirelim
             Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();
             if (arrowRigidbody != null)
             {
                 arrowRigidbody.AddForce(arrowSpawnPoint.forward * force);
             }
        
            
    }
}

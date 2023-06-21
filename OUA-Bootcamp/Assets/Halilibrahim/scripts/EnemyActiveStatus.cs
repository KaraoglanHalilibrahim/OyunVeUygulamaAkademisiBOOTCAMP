using UnityEngine;

public class EnemyActiveStatus : MonoBehaviour
{
    public GameObject enemyObject; // Aktiflik durumu deðiþtirilecek düþman nesnesi
    public float deactivationTime = 3f; // Deaktivasyon süresi (saniye)

    private void Start()
    {
        DeactivateEnemy();
    }

    private void DeactivateEnemy()
    {
        enemyObject.SetActive(false);
        Invoke("ActivateEnemy", deactivationTime);
    }

    private void ActivateEnemy()
    {
        enemyObject.SetActive(true);
    }
}

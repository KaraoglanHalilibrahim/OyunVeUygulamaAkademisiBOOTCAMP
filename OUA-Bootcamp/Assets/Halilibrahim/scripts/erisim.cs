using UnityEngine;

public class Erisim : MonoBehaviour
{
    private GameObject enemy;
    private enemy_death enemyDeathScript;

    private void Start()
    {
        // Enemy01 objesini buluyoruz
        enemy = GameObject.FindWithTag("Enemy");

        if (enemy != null)
        {
            // enemy_death scriptine eriþiyoruz
            enemyDeathScript = enemy.GetComponentInChildren<enemy_death>();

            if (enemyDeathScript != null)
            {
                // health deðiþkenini kullanabilirsiniz
                int health = enemyDeathScript.health;

                // health deðiþkenini kullanarak bir þeyler yapabilirsiniz
                Debug.Log("Enemy01'in baþlangýç saðlýðý: " + health);
            }
            else
            {
                Debug.Log("Enemy01 objesinde EnemyDeath scripti bulunamadý.");
            }
        }
        else
        {
            Debug.Log("Enemy01 objesi bulunamadý.");
        }
    }

    private void Update()
    {
        // Update() fonksiyonu her frame'de çaðrýlýr
        if (enemyDeathScript != null)
        {
            // health deðiþkenini sürekli olarak güncelleyebilirsiniz
            int health = enemyDeathScript.health;

            // health deðiþkenini kullanarak bir þeyler yapabilirsiniz
            Debug.Log("Enemy01'in güncel saðlýðý: " + health);
        }
    }
}

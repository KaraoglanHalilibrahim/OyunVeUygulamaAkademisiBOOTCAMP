using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject mermiPrefab;
    public Transform nisangahNoktasi;

    public float atisGucu = 1000f;
    public float atisHizi = 100f;

    private int ammo = 4;
    private bool atisYapilabilir = true;
    private bool reload = false; // reload boolean'ý eklendi
    private float zamanSuresi = 1f;
    private float reloadSure = 5f; // Reload süresi

    private float sonAtisZamani;

    void Start()
    {
        sonAtisZamani = -zamanSuresi;
    }

    void Update()
    {
        if (reload) // Reload durumunda atýþ yapmaya izin verme
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && atisYapilabilir && ammo > 0 && Time.time > sonAtisZamani + zamanSuresi)
        {
            AtisYap();
            ammo--;
            sonAtisZamani = Time.time;

            if (ammo == 0)
            {
                atisYapilabilir = false;
                StartCoroutine(ReloadDelay()); // ReloadDelay Coroutine'ini baþlat
            }
        }
    }

    void AtisYap()
    {
        GameObject mermi = Instantiate(mermiPrefab, nisangahNoktasi.position, nisangahNoktasi.rotation);
        mermi.GetComponent<Rigidbody>().velocity = nisangahNoktasi.forward * atisHizi;
        mermi.GetComponent<Rigidbody>().AddForce(nisangahNoktasi.forward * atisGucu);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShotgunBullet"))
        {
            ammo += 4;
            Destroy(other.gameObject);
            StartCoroutine(ReloadDelay()); // ReloadDelay Coroutine'ini baþlat
        }
    }

    private System.Collections.IEnumerator ReloadDelay()
    {
        reload = true;
        yield return new WaitForSeconds(reloadSure);
        reload = false;
        ammo = 4;
        atisYapilabilir = true;
    }
}

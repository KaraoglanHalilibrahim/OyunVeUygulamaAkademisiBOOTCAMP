using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject mermiPrefab;
    public Transform nisangahNoktasi;

    public float atisGucu = 1000f;
    public float atisHizi = 100f;

    public int ammo = 0;
    private bool atisYapilabilir = true;
    private bool reload = false; // reload boolean'ý eklendi
    private float zamanSuresi = 1f;

    private float sonAtisZamani;

    void Start()
    {
        sonAtisZamani = -zamanSuresi;
    }

    void Update()
    {
        if (reload || ammo == 0) // Reload durumunda veya mermi kalmadýðýnda atýþ yapmaya izin verme
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && atisYapilabilir && Time.time > sonAtisZamani + zamanSuresi)
        {
            AtisYap();
            ammo--;
            sonAtisZamani = Time.time;

            if (ammo == 0)
            {
                atisYapilabilir = false;
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
            atisYapilabilir = true;
        }
    }

    
}

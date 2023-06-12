using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject mermiPrefab; // Kurþun objesinin prefab'ý
    public Transform nisangahNoktasi; // Nisangah noktasýnýn Transform bileþeni

    public float atisGucu = 10000f; // Kurþun atýþ gücü (N)
    public float atisHizi = 1000f; // Kurþun atýþ hýzý (m/s)

    private int ammo = 10; // Ammo miktarý ve baþlangýç deðeri
    private bool atisYapilabilir = true; // Atýþ yapýlabilir durumu kontrol etmek için
    private float zamanSuresi = 1f; // Atýþlar arasýndaki zaman sýnýrý

    private float sonAtisZamani; // Son atýþýn yapýldýðý zaman

    void Start()
    {
        sonAtisZamani = -zamanSuresi; // Oyun baþlangýcýnda son atýþ zamanýný sýfýrla
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && atisYapilabilir && ammo > 0 && Time.time > sonAtisZamani + zamanSuresi) // Sol fare tuþuna basýldýðýnda, atýþ yapýlabilir durumdaysa, ammo miktarý 0'dan büyükse ve zaman sýnýrý geçilmiþse atýþ yap
        {
            AtisYap();
            ammo--; // Ammo miktarýný 1 azalt
            sonAtisZamani = Time.time; // Son atýþ zamanýný güncelle

            if (ammo == 0)
            {
                atisYapilabilir = false; // Ammo miktarý 0 olduðunda atýþ yapýlabilir durumu false yap
            }
        }
    }

    void AtisYap()
    {
        // Mermi objesini oluþtur ve nisangah noktasýna yerleþtir
        GameObject mermi = Instantiate(mermiPrefab, nisangahNoktasi.position, nisangahNoktasi.rotation);

        // Kurþuna hýz uygula
        mermi.GetComponent<Rigidbody>().velocity = nisangahNoktasi.forward * atisHizi;

        // Kurþuna bir kuvvet uygula (opsiyonel)
        mermi.GetComponent<Rigidbody>().AddForce(nisangahNoktasi.forward * atisGucu);
    }
}

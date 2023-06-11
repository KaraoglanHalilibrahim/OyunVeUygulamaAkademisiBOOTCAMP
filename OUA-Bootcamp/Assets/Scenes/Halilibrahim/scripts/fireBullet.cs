using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject mermiPrefab; // Kurþun objesinin prefab'ý
    public Transform nisangahNoktasi; // Nisangah noktasýnýn Transform bileþeni

    public float atisGucu = 1000f; // Kurþun atýþ gücü (N)
    public float atisHizi = 100f; // Kurþun atýþ hýzý (m/s)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýðýnda atýþ yap
        {
            AtisYap();
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

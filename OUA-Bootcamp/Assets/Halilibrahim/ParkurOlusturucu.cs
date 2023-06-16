using UnityEngine;

public class ParkurOluþturucu : MonoBehaviour
{
    public GameObject küpPrefab; // Kullanacaðýmýz küp prefabý
    public GameObject platformPrefab; // Kullanacaðýmýz platform prefabý

    public int küpSayýsý = 10; // Oluþturulacak küp sayýsý
    public float aralýkMesafesi = 2f; // Küpler ve platformlar arasýndaki mesafe
    public Vector3 küpBoyutlarý = new Vector3(1f, 1f, 1f); // Küp boyutlarý
    public float yükseklikAralýðý = 1.5f; // Küplerin rastgele yükseklik aralýðý

    private void Start()
    {
        ParkurOluþtur();
    }

    void ParkurOluþtur()
    {
        Vector3 spawnPozisyonu = transform.position; // Baþlangýç noktasý

        for (int i = 0; i < küpSayýsý; i++)
        {
            if (i % 4 == 0) // Her 4. elemaný platform olarak yerleþtir
            {
                GameObject yeniPlatform = Instantiate(platformPrefab, spawnPozisyonu, Quaternion.identity); // Yeni platform oluþtur

                // Platform boyutlarýný ayarla
                yeniPlatform.transform.localScale = new Vector3(aralýkMesafesi, küpBoyutlarý.y, aralýkMesafesi);

                // Platformu yere yerleþtir
                yeniPlatform.transform.position = new Vector3(spawnPozisyonu.x, küpBoyutlarý.y / 2f, spawnPozisyonu.z);
            }
            else
            {
                GameObject yeniKüp = Instantiate(küpPrefab, spawnPozisyonu, Quaternion.identity); // Yeni küp oluþtur

                // Küp boyutlarýný ayarla
                yeniKüp.transform.localScale = küpBoyutlarý;

                // Küpü yere yerleþtir
                yeniKüp.transform.position = new Vector3(spawnPozisyonu.x, küpBoyutlarý.y / 2f, spawnPozisyonu.z);

                // Rastgele yükseklik deðeri uygula
                float rastgeleYükseklik = Random.Range(0f, yükseklikAralýðý);
                yeniKüp.transform.position += new Vector3(0f, rastgeleYükseklik, 0f);
            }

            spawnPozisyonu.x += aralýkMesafesi; // Küpleri ve platformlarý yatayda birbirinden ayýr
        }
    }
}

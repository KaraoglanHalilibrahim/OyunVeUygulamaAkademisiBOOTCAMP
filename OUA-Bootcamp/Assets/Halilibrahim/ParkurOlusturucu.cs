using UnityEngine;

public class ParkurOlusturucu : MonoBehaviour
{
    public GameObject parkurParcasiPrefab; // Parkur parçasý prefabý
    public Transform parkurParent; // Parkur parçalarýnýn ekleneceði ebeveyn transform

    public int genislik = 10; // Parkurun geniþliði
    public int uzunluk = 20; // Parkurun uzunluðu
    public int yukseklik = 5; // Parkurun yüksekliði

    void Start()
    {
        OyunParkurunuOlustur();
    }

    void OyunParkurunuOlustur()
    {
        for (int x = 0; x < genislik; x++)
        {
            for (int z = 0; z < uzunluk; z++)
            {
                for (int y = 0; y < yukseklik; y++)
                {
                    GameObject parkurParcasi = Instantiate(parkurParcasiPrefab, new Vector3(x, y, z), Quaternion.identity);
                    parkurParcasi.transform.parent = parkurParent;
                }
            }
        }
    }
}

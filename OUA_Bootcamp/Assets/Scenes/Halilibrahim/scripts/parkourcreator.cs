using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parkourcreator : MonoBehaviour
{
    public Transform parkurNesnesi;
    public float hareketHizi = 2f;
    public float alttaGelmeYuksekligi = -10f;

    private Vector3[] baslangicKonumlari;
    private bool kaydiriliyor = false;
    private float kaymaZamani = 0f;

    void Start()
    {
        OyunuBaþlat();
    }

    void Update()
    {
        if (kaydiriliyor)
        {
            kaymaZamani += Time.deltaTime * hareketHizi;

            for (int i = 0; i < parkurNesnesi.childCount; i++)
            {
                Transform obje = parkurNesnesi.GetChild(i);
                Vector3 hedefKonum = baslangicKonumlari[i];
                Vector3 yeniKonum = Vector3.Lerp(obje.position, hedefKonum, kaymaZamani);
                obje.position = yeniKonum;
            }

            if (kaymaZamani >= 1f)
            {
                kaydiriliyor = false;
            }
        }
    }

    void OyunuBaþlat()
    {
        int objeSayisi = parkurNesnesi.childCount;
        baslangicKonumlari = new Vector3[objeSayisi];

        // Baþlangýç konumlarýný kaydet
        for (int i = 0; i < objeSayisi; i++)
        {
            baslangicKonumlari[i] = parkurNesnesi.GetChild(i).position;
        }

        Kaydir();
    }

    private void Kaydir()
    {
        for (int i = 0; i < parkurNesnesi.childCount; i++)
        {
            Transform obje = parkurNesnesi.GetChild(i);
            Vector3 rastgeleKonum = new Vector3(Random.Range(-10f, 100f), alttaGelmeYuksekligi, Random.Range(-10f, 100f));
            obje.position = rastgeleKonum;
        }

        kaydiriliyor = true;
        kaymaZamani = 0f;
    }
}




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
    private float beklemeyeBaslamaZamani = 0f;

    void Start()
    {
        OyunuBaslat();
    }

    void Update()
    {
        if (kaydiriliyor)
        {
            float gecenSure = Time.time - beklemeyeBaslamaZamani;

            if (gecenSure >= 0f)
            {
                for (int i = 0; i < parkurNesnesi.childCount; i++)
                {
                    Transform obje = parkurNesnesi.GetChild(i);
                    Vector3 hedefKonum = baslangicKonumlari[i];
                    Vector3 yeniKonum = Vector3.MoveTowards(obje.position, hedefKonum, hareketHizi * Time.deltaTime);
                    obje.position = yeniKonum;
                }

                bool tamamlananHareket = true;
                for (int i = 0; i < parkurNesnesi.childCount; i++)
                {
                    Transform obje = parkurNesnesi.GetChild(i);
                    if (Vector3.Distance(obje.position, baslangicKonumlari[i]) > 0.01f)
                    {
                        tamamlananHareket = false;
                        break;
                    }
                }

                if (tamamlananHareket)
                {
                    kaydiriliyor = false;
                }
            }
        }
    }

    void OyunuBaslat()
    {
        int objeSayisi = parkurNesnesi.childCount;
        baslangicKonumlari = new Vector3[objeSayisi];

        // Baþlangýç konumlarýný kaydet
        for (int i = 0; i < objeSayisi; i++)
        {
            baslangicKonumlari[i] = parkurNesnesi.GetChild(i).position;
        }

        RastgeleKonumdaHareketEt();
    }

    private void RastgeleKonumdaHareketEt()
    {
        for (int i = 0; i < parkurNesnesi.childCount; i++)
        {
            Transform obje = parkurNesnesi.GetChild(i);
            Vector3 rastgeleKonum = new Vector3(Random.Range(-100f, 100f), alttaGelmeYuksekligi, Random.Range(-100f, 100f));
            obje.position = rastgeleKonum;
        }

        kaydiriliyor = true;
        beklemeyeBaslamaZamani = Time.time;
    }
}

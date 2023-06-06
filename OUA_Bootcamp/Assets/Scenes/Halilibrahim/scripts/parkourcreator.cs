using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parkourcreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OyunuBaþlat();
    }

    void OyunuBaþlat()
    {
        for (int i = 0; i < parkurNesnesi.childCount; i++)
        {
            Transform obje = parkurNesnesi.GetChild(i);

            // Rastgele bir konum oluþtur
            Vector3 rastgeleKonum = new Vector3(Random.Range(-rastgeleKonumAralýðý, rastgeleKonumAralýðý),
                                                0f,
                                                Random.Range(-rastgeleKonumAralýðý, rastgeleKonumAralýðý));

            // Objeyi rastgele konuma taþý
            obje.position = obje.position + rastgeleKonum;
        }
    }
}


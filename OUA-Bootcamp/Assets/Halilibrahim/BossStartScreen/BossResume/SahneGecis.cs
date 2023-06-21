using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    public int GecisZamani = 17;
    public string hedefSahneAdi; // Geçiþ yapýlacak hedef sahne adý

    private void Start()
    {
        Invoke("SahneyeGec", GecisZamani); // 20 saniye sonra "SahneyeGec" metodunu çaðýr
    }

    private void SahneyeGec()
    {
        SceneManager.LoadScene(hedefSahneAdi); // Hedef sahneye geçiþ yap
    }
}

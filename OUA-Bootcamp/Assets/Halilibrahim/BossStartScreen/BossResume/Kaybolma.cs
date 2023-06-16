using UnityEngine;

public class Kaybolma : MonoBehaviour
{
    public float kaybolmaZamani = 11f;

    private void Start()
    {
        Invoke("DeaktiveEt", kaybolmaZamani);
    }

    private void DeaktiveEt()
    {
        gameObject.SetActive(false);
    }
}

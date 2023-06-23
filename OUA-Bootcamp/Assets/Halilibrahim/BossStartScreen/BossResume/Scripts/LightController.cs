using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light pointLight;
    public float minColorValue = 35.0f; // Minimum renk deðeri
    public float maxColorValue = 255.0f; // Maksimum renk deðeri
    public float changeSpeed = 50.0f; // Deðer deðiþtirme hýzý

    private Color currentColor;
    private float targetR;
    private float targetG;
    private float targetB;

    void Start()
    {
        // Baþlangýç deðerlerini atama
        currentColor = new Color(Random.Range(minColorValue, maxColorValue) / 255.0f, Random.Range(minColorValue, maxColorValue) / 255.0f, Random.Range(minColorValue, maxColorValue) / 255.0f);
        targetR = Random.Range(minColorValue, maxColorValue) / 255.0f;
        targetG = Random.Range(minColorValue, maxColorValue) / 255.0f;
        targetB = Random.Range(minColorValue, maxColorValue) / 255.0f;
    }

    void Update()
    {
        // Renk deðerlerini hedeflere doðru deðiþtirme
        currentColor.r = Mathf.MoveTowards(currentColor.r, targetR, changeSpeed * Time.deltaTime / 255.0f);
        currentColor.g = Mathf.MoveTowards(currentColor.g, targetG, changeSpeed * Time.deltaTime / 255.0f);
        currentColor.b = Mathf.MoveTowards(currentColor.b, targetB, changeSpeed * Time.deltaTime / 255.0f);

        // Hedef renklere ulaþýldýðýnda yeni hedefler belirleme
        if (currentColor.r == targetR && currentColor.g == targetG && currentColor.b == targetB)
        {
            targetR = Random.Range(minColorValue, maxColorValue) / 255.0f;
            targetG = Random.Range(minColorValue, maxColorValue) / 255.0f;
            targetB = Random.Range(minColorValue, maxColorValue) / 255.0f;
        }

        // Renk deðerlerini deðiþtirme
        ChangeLightColor(currentColor);
    }

    void ChangeLightColor(Color color)
    {
        // Iþýk rengini deðiþtirme
        pointLight.color = color;
    }
}

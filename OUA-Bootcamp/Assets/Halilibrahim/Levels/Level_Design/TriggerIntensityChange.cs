using UnityEngine;

public class TriggerIntensityChange : MonoBehaviour
{
    public Light directionalLight;
    public GameObject targetObject;
    public float intensityValue = 1000000000000f;
    public float changeDuration = 5f;
    private float initialIntensity;
    private float targetIntensity;
    private float startTime;

    private void Start()
    {
        initialIntensity = directionalLight.intensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            targetIntensity = intensityValue;
            startTime = Time.time;
        }
    }

    private void Update()
    {
        if (targetIntensity > initialIntensity)
        {
            float elapsedTime = Time.time - startTime;
            if (elapsedTime < changeDuration)
            {
                float intensityProgress = elapsedTime / changeDuration;
                directionalLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, intensityProgress);
            }
            else
            {
                directionalLight.intensity = targetIntensity;
                targetIntensity = initialIntensity;
            }
        }
    }
}

using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Eðer obje triger alanýna girdiyse ve "Character" etiketine sahipse
        if (other.CompareTag("Character"))
        {
            foreach (GameObject obj in objectsToDisable)
            {
                // Objelerin aktifliðini kapat
                obj.SetActive(false);
            }

            // AudioSource'i oynat
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Eðer obje triger alanýndan çýktýysa ve "Character" etiketine sahipse
        if (other.CompareTag("Character"))
        {
            foreach (GameObject obj in objectsToDisable)
            {
                // Objelerin aktifliðini aç
                obj.SetActive(true);
            }

            // AudioSource'i durdur
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}

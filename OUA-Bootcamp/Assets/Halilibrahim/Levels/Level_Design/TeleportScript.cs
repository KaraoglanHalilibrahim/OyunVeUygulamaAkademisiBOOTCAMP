using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public GameObject teleportTarget; // Iþýnlanma hedefi olarak belirleyeceðimiz obje
    public GameObject targetObject; // Iþýnlanacak objeyi belirleyeceðimiz obje

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            // Iþýnlanma iþlemi
            targetObject.transform.position = teleportTarget.transform.position;
        }
    }
}

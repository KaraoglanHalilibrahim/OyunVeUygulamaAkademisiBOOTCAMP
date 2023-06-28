using UnityEngine;

public class ArcherFocus : MonoBehaviour
{
    public Transform target; // Karakterin bakmasý gereken hedef obje
    public float rotationSpeed = 5f; // Dönüþ hýzý

    private void Update()
    {
        if (target != null)
        {
            // Karakterin hedef objeye doðru dönmesini saðla
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0f); // Z ekseni (ileri-geri) sýfýrlanýr
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}

using UnityEngine;

public class Pushing : MonoBehaviour
{
    public float pushDistance = 3f; // Ýleriye itme mesafesi

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PushCharacter();
        }
    }

    private void PushCharacter()
    {
        // Karakterin bulunduðu yönü al ve ona göre itme mesafesini uygula
        Vector3 pushDirection = transform.forward;
        Vector3 targetPosition = transform.position + pushDirection * pushDistance;

        // Karakteri hedef pozisyona taþý
        transform.position = targetPosition;
    }
}

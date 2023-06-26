using UnityEngine;

public class MouseUnfocus : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true; // Fare imleci görünür yapýlýyor
        Cursor.lockState = CursorLockMode.None; // Fare imleci serbest býrakýlýyor
    }
}

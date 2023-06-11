using UnityEngine;

public class MouseFocus : MonoBehaviour
{
    void Start()
    {
        // Oyun ekranýna odaklan
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Esc tuþuna basýldýðýnda fareyi serbest býrak
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

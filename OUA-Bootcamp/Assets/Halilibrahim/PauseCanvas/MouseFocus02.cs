using UnityEngine;

public class MouseFocus02 : MonoBehaviour
{
    private bool hasFocus = true;

    private void Start()
    {
        // Baþlangýçta mouse focus'u aktif hale getir
        SetMouseFocus(true);
    }

    private void Update()
    {
        // Tab tuþuna basýldýðýnda focus durumunu deðiþtir
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hasFocus = !hasFocus;
            SetMouseFocus(hasFocus);
        }
    }

    private void SetMouseFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked; // Mouse kilitli
            Cursor.visible = false; // Mouse görünmez
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Mouse serbest
            Cursor.visible = true; // Mouse görünür
        }
    }
}

using UnityEngine;

public class InputDurdur : MonoBehaviour
{
    public MonoBehaviour script; // Deaktive edilecek script

    private void Start()
    {
        Invoke("DeaktiveEt", 5f);
    }

    private void DeaktiveEt()
    {
        if (script != null)
        {
            script.enabled = false;
        }
    }
}

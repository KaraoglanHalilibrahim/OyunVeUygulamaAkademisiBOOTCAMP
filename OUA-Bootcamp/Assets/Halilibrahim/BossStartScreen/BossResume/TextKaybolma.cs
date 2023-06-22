using UnityEngine;
using TMPro;

public class TextKaybolma : MonoBehaviour
{
    public GameObject otherObject;
    public TMP_Text textToDisappear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == otherObject)
        {
            textToDisappear.gameObject.SetActive(false);
        }
    }
}

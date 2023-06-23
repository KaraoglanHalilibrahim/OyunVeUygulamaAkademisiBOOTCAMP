using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAktifleşme : MonoBehaviour
{
    public GameObject otherObject;
    public TMP_Text textToDisappear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == otherObject)
        {
            textToDisappear.gameObject.SetActive(true);
        }
    }
}

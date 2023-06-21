using UnityEngine;

public class CharacterDeactive : MonoBehaviour
{
    public GameObject characterObject;
    public float deactivationDelay = 25f;

    private float timer;

    private void Start()
    {
        timer = deactivationDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            characterObject.SetActive(false);
            enabled = false; // Script'i devre dýþý býrakmak için
        }
    }
}

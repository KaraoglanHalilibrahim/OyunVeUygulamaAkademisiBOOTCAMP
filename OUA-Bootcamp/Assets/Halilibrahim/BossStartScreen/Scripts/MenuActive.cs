using UnityEngine;

public class MenuActive : MonoBehaviour
{
    public GameObject mainMenuObject;
    public float activationDelay = 25f;
    private float timer;
    private bool activated = false;

    private void Start()
    {
        mainMenuObject.SetActive(false);
        timer = activationDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && !activated)
        {
            mainMenuObject.SetActive(true);
            activated = true;
        }
    }
}

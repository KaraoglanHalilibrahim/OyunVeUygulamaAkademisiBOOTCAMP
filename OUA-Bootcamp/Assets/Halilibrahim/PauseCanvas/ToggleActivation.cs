using UnityEngine;

public class ToggleActivation : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.Tab;
    public GameObject[] deactivateObjects;
    public GameObject[] activateObjects;

    private bool isActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isActive = !isActive;


            foreach (GameObject obj in deactivateObjects)
            {
                obj.SetActive(!isActive);
            }

            foreach (GameObject obj in activateObjects)
            {
                obj.SetActive(isActive);
            }
        }
    }
}

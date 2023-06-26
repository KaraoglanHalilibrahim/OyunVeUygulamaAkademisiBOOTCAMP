using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject[] deactivationObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            foreach (GameObject obj in deactivationObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}

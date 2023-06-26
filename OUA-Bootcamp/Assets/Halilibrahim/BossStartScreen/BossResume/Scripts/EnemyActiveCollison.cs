using UnityEngine;

public class EnemyActiveCollision : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject[] activationObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            foreach (GameObject obj in activationObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}

using UnityEngine;

public class BoyScaret : MonoBehaviour
{
    public string belirliKarakterTag;
    public Animator animatör;
    public string booleanAdý;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(belirliKarakterTag))
        {
            animatör.SetBool(booleanAdý, true);
        }
    }
}

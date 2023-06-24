using UnityEngine;
using TMPro;

public class DeadEnemy : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Animator animator;
    public string parameterName;

    void Update()
    {
        // Animator'daki parametrenin deðerini al
        int value = animator.GetInteger(parameterName);

        // TextMeshPro bileþenine deðeri aktar
        textMeshPro.text = value.ToString();
    }
}

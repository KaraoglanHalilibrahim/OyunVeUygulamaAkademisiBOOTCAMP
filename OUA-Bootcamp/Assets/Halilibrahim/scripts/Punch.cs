using UnityEngine;

public class Punch : MonoBehaviour
{
    public Animator animator;
    private bool punch = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            punch = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            punch = false;
        }

        animator.SetBool("punch", punch);
    }
}

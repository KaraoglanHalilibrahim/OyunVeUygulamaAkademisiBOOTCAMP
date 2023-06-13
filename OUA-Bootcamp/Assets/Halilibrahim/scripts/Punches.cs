using UnityEngine;

public class Punches : MonoBehaviour
{
    public Animator animator;
    private bool leftPunch;
    private bool rightPunch;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftPunch = true;
            animator.SetBool("LeftPunch", leftPunch);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftPunch = false;
            animator.SetBool("LeftPunch", leftPunch);
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightPunch = true;
            animator.SetBool("RightPunch", rightPunch);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightPunch = false;
            animator.SetBool("RightPunch", rightPunch);
        }
    }
}

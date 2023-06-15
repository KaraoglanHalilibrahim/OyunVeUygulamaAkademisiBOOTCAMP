using UnityEngine;

public class Punches : MonoBehaviour
{
    public Animator animator;
    private bool leftPunch;
    private bool rightPunch;
    private bool Run;

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

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            Run = true;
            animator.SetBool("Run", Run);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            Run = false;
            animator.SetBool("Run", Run);
        }
    }
}

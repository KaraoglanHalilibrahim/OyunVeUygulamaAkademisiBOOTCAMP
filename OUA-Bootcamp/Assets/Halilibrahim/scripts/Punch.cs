using UnityEngine;

public class Punch : MonoBehaviour
{
    public Animator animator;
    private bool leftPunch = false;
    private bool rightPunch = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftPunch = true;
            rightPunch = false;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rightPunch = true;
            leftPunch = false;
        }
        else
        {
            leftPunch = false;
            rightPunch = false;
        }

        animator.SetBool("leftpunch", leftPunch);
        animator.SetBool("rightpunch", rightPunch);
    }
}

using System.Collections;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public Animator animator;

    private bool leftPunch = false;
    private bool rightPunch = false;

    private bool canLeftPunch = true;
    private bool canRightPunch = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canLeftPunch)
        {
            leftPunch = true;
            animator.SetBool("LeftPunch", leftPunch);
            canLeftPunch = false;
            StartCoroutine(EnableLeftPunch());
        }
        if (Input.GetMouseButtonDown(1) && canRightPunch)
        {
            rightPunch = true;
            animator.SetBool("RightPunch", rightPunch);
            canRightPunch = false;
            StartCoroutine(EnableRightPunch());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(DelayedExecutionLeft());
        }
        if (Input.GetMouseButtonUp(1))
        {
            StartCoroutine(DelayedExecutionRight());
        }
    }

    private IEnumerator DelayedExecutionLeft()
    {
        yield return new WaitForSeconds(0f);

        leftPunch = false;
        animator.SetBool("LeftPunch", leftPunch);

        Debug.Log("Sol yumruk: " + leftPunch);
    }

    private IEnumerator DelayedExecutionRight()
    {
        yield return new WaitForSeconds(0f);

        rightPunch = false;
        animator.SetBool("RightPunch", rightPunch);

        Debug.Log("Sað yumruk: " + rightPunch);
    }

    private IEnumerator EnableLeftPunch()
    {
        yield return new WaitForSeconds(0f);
        canLeftPunch = true;
    }

    private IEnumerator EnableRightPunch()
    {
        yield return new WaitForSeconds(0f);
        canRightPunch = true;
    }
}

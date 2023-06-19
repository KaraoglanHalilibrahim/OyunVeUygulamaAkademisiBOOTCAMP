using UnityEngine;

public class WallColliderRight : MonoBehaviour
{
    public Animator animatorPunch;
    public Animator animatorShotgun;
    private bool RightWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            RightWall = true;
            animatorPunch.SetBool("RightWall", RightWall);
            animatorShotgun.SetBool("RightWall", RightWall);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            RightWall = false;
            animatorPunch.SetBool("RightWall", RightWall);
            animatorShotgun.SetBool("RightWall", RightWall);
        }
    }
}

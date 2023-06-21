using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public Animator animatorPunch;
    public Animator animatorShotgun;
    private bool LeftWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            LeftWall = true;
            animatorPunch.SetBool("LeftWall", LeftWall);
            animatorShotgun.SetBool("LeftWall", LeftWall);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            LeftWall = false;
            animatorPunch.SetBool("LeftWall", LeftWall);
            animatorShotgun.SetBool("LeftWall", LeftWall);

        }
    }
}

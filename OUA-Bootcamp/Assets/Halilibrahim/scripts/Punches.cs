using UnityEngine;

public class Punches : MonoBehaviour
{
    public bool LeftPunch;
    public bool RightPunch;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Sol yumruk tuþuna basýlýrsa LeftPunch'ý true yap
        if (Input.GetMouseButtonDown(0))
        {
            LeftPunch = true;
            RightPunch = false;
        }

        // Sað yumruk tuþuna basýlýrsa RightPunch'ý true yap
        if (Input.GetMouseButtonDown(1))
        {
            RightPunch = true;
            LeftPunch = false;
        }

        // Animatördeki parametreleri güncelle
        animator.SetBool("LeftPunch", LeftPunch);
        animator.SetBool("RightPunch", RightPunch);

        // Animasyon tamamlandýðýnda boolean deðerleri sýfýrla
        if (LeftPunch || RightPunch)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("LeftPunch") || stateInfo.IsName("RightPunch"))
            {
                LeftPunch = false;
                RightPunch = false;
            }
        }
    }
}

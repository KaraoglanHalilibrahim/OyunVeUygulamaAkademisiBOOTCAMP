using UnityEngine;

public class PunchColliderRight : MonoBehaviour
{
    public GameObject PunchColliderLeftObject;
    private Animator animator;
    private bool isColliderActive;

    private void Start()
    {
        // Baþlangýçta objeyi deaktif hale getir
        PunchColliderLeftObject.SetActive(false);
        animator = GetComponent<Animator>();
        isColliderActive = false;
    }

    private void Update()
    {
        // Animatördeki RightPunch deðeri true ise
        if (animator.GetBool("RightPunch"))
        {
            // PunchColliderLeft objesini aktif hale getir ve zamanlayýcýyý baþlat
            if (!isColliderActive)
            {
                PunchColliderLeftObject.SetActive(true);
                isColliderActive = true;
                Invoke("DeactivateCollider", 0.1f);
            }
        }
        else
        {
            // PunchColliderLeft objesini deaktif hale getir
            PunchColliderLeftObject.SetActive(false);
            isColliderActive = false;
        }
    }

    private void DeactivateCollider()
    {
        // PunchColliderLeft objesini deaktif hale getir
        PunchColliderLeftObject.SetActive(false);
        isColliderActive = false;
    }
}

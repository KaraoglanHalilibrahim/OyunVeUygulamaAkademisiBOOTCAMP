using UnityEngine;

public class PunchColliderLeft : MonoBehaviour
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
        // Animatördeki LeftPunch deðeri true ise
        if (animator.GetBool("LeftPunch"))
        {
            // PunchColliderLeft objesini aktif hale getir ve zamanlayýcýyý baþlat
            if (!isColliderActive)
            {
                PunchColliderLeftObject.SetActive(true);
                isColliderActive = true;
                Invoke("DeactivateCollider", 0.7f);
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

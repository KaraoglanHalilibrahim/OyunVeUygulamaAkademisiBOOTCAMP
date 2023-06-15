using UnityEngine;

public class PunchColliderRight : MonoBehaviour
{
    public GameObject PunchColliderLeftObject;

    private void Start()
    {
        // Baþlangýçta objeyi deaktif hale getir
        PunchColliderLeftObject.SetActive(false);
    }

    private void Update()
    {
        // Sol mouse tuþuna basýldýðýnda
        if (Input.GetMouseButtonDown(1))
        {
            // PunchColliderLeft objesini aktif hale getir
            PunchColliderLeftObject.SetActive(true);

            // 0.7 saniye sonra objeyi tekrar deaktif hale getir
            Invoke("DeactivateCollider", 0.7f);
        }
    }

    private void DeactivateCollider()
    {
        // PunchColliderLeft objesini deaktif hale getir
        PunchColliderLeftObject.SetActive(false);
    }
}

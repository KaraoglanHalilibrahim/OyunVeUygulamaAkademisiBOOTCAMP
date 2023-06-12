using UnityEngine;
using UnityEngine.UI;

public class weapon_shotgun : MonoBehaviour
{
    private int ammo = 4;
    private bool shoot;
    private bool canShoot = true;
    private bool reload = false;
    private float shootCooldown = 1.0f;
    private float reloadTime = 5.0f; // Yeniden yükleme süresi
    private Animator animator;
    public Text ammoText;
    public AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        UpdateAmmoText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && !reload) // reload true deðilken atýþ yapmaya izin verme
        {
            if (ammo > 0)
            {
                shoot = true;
                ammo--;
                canShoot = false;
                Invoke("ResetShoot", shootCooldown);
            }
        }
        else
        {
            shoot = false;
        }

        animator.SetBool("shoot?", shoot);
        animator.SetBool("reload", reload);

        if (shoot)
        {
            audioSource.Play();
        }

        UpdateAmmoText();
    }

    private void ResetShoot()
    {
        canShoot = true;
    }

    private void UpdateAmmoText()
    {
        ammoText.text = " " + ammo.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShotgunBullet"))
        {
            ammo += 4;
            Destroy(other.gameObject);
            UpdateAmmoText();
            StartCoroutine(ReloadAnimation());
        }
    }

    private System.Collections.IEnumerator ReloadAnimation()
    {
        reload = true;
        yield return new WaitForSeconds(reloadTime);
        reload = false;
    }
}

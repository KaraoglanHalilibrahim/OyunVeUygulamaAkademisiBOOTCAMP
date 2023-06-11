using UnityEngine;
using UnityEngine.UI;

public class weapon_shotgun : MonoBehaviour
{
    private int ammo = 10;
    private bool reload;
    private bool shoot;
    private bool canShoot = true; // Fareye tekrar basýlabilmesini kontrol etmek için bir deðiþken
    private bool canReload = true; // Reload iþlemi için bekleme kontrolü
    private float shootCooldown = 1.5f; // Tekrar ateþ edebilmek için geçmesi gereken zaman
    private float reloadCooldown = 4f; // Reload iþlemi için geçmesi gereken zaman
    private float disableShootTime = 5f; // R tuþuna basýldýðýnda sol fare düðmesine basmanýn engelleneceði süre
    private Animator animator;
    public Text ammoText; // Ammo deðerini gösterecek olan UI Text nesnesi

    private static readonly int ReloadHash = Animator.StringToHash("reload");

    private void Start()
    {
        animator = GetComponent<Animator>();
        UpdateAmmoText(); // Ammo deðerini baþlangýçta güncelle
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            if (ammo > 0)
            {
                shoot = true;
                ammo--;
                canShoot = false; // Ateþ edildiðinde tekrar atýþ yapabilmek için bekleme baþlar
                Invoke("ResetShoot", shootCooldown); // ShootCooldown süresi sonunda ResetShoot metodu çaðrýlýr
            }
            else
            {
                reload = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && ammo < 10 && canReload)
        {
            reload = true;
            canShoot = false; // R tuþuna basýldýðýnda sol fare düðmesine basmanýn engellenmesi için canShoot'u false yapar
            Invoke("ResetReload", reloadCooldown); // ReloadCooldown süresi sonunda ResetReload metodu çaðrýlýr
            Invoke("EnableShoot", disableShootTime); // disableShootTime süresi sonunda EnableShoot metodu çaðrýlýr
        }
        else
        {
            shoot = false;
            reload = false;
        }

        animator.SetBool("shoot?", shoot);
        animator.SetBool(ReloadHash, reload);

        if (reload)
        {
            ammo = 10;
            reload = false;
            canShoot = false; // Reload iþlemi baþladýðýnda tekrar atýþ yapabilmek için bekleme baþlar
            Invoke("ResetShoot", reloadCooldown); // ReloadCooldown süresi sonunda ResetShoot metodu çaðrýlýr
        }

        UpdateAmmoText(); // Ammo deðerini güncelle
    }

    private void ResetShoot()
    {
        canShoot = true; // Bekleme süresi bittiðinde tekrar atýþ yapýlabilir hale gelir
    }

    private void ResetReload()
    {
        canReload = true; // Reload iþlemi tamamlandýðýnda tekrar R tuþuna basýlabilmesi için canReload'u true yapar
    }

    private void EnableShoot()
    {
        canShoot = true; // R tuþuna basýldýktan sonra sol fare düðmesine basýlabilmesi için canShoot'u true yapar
    }

    private void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + ammo.ToString(); // Ammo deðerini UI Text nesnesine aktar
    }
}

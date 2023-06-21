using UnityEngine;

public class SwitchGuns : MonoBehaviour
{
    public GameObject MainCharacterArms;
    public GameObject Arm_remington;

    void Start()
    {
        // Baþlangýçta Arm_remington objesini deaktif hale getir
        Arm_remington.SetActive(false);
    }

    void Update()
    {
        // 1 tuþuna basýldýðýnda MainCharacterArms deaktif, Arm_remington aktif hale gelsin
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MainCharacterArms.SetActive(true);
            Arm_remington.SetActive(false);
        }

        // 2 tuþuna basýldýðýnda MainCharacterArms aktif, Arm_remington deaktif hale gelsin
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MainCharacterArms.SetActive(false);
            Arm_remington.SetActive(true);
        }
    }
}

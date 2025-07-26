using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public WeaponData selectedWeapon;
    public WeaponData pistol;
    public WeaponData doubleBarrel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public WeaponData SelectWeapon(WeaponData weapon)
    {
        return selectedWeapon = weapon;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour
{
    public Button pistolButton;
    public Button doubleBarrelButton;
    public WeaponData pistol;
    public WeaponData doubleBarrel;
    public static WeaponSelect Instance;
    public GameObject player;

    private Weapon weaponComponent;

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
        }
        weaponComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
        pistolButton.onClick.AddListener(() => SelectWeapon(pistol));
        doubleBarrelButton.onClick.AddListener(() => SelectWeapon(doubleBarrel));
    }


    void SelectWeapon(WeaponData weapon)
    {
        //weaponComponent = weapon;
    }
}

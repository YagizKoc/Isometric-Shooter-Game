// WeaponSelectUI.cs
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    public Button pistolButton;
    public Button doubleBarrelButton;
    public WeaponData pistol;
    public WeaponData doubleBarrel;

    void Start()
    {
        pistolButton.onClick.AddListener(() => WeaponManager.Instance.SelectWeapon(pistol));
        doubleBarrelButton.onClick.AddListener(() => WeaponManager.Instance.SelectWeapon(doubleBarrel));
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    //public WeaponData weaponData;
    public WeaponData pistol;
    public WeaponData doubleBarrel;
    public WeaponData playerWeapon;
    private WeaponManager weaponManager;
    [SerializeField] GameObject weaponSlot;
    public Transform firePoint;
    private PlayerStats playerStats;
    [SerializeField] AudioSource weaponAudioSource;

    private float fireCooldown;

    private void Start()
    {
        //Selecting Weapon
        weaponManager = GameObject.Find("Weapon Manager")?.GetComponent<WeaponManager>();

        //for testing purposes playerWeapon is set to fix value and do not delete lines below!
        playerWeapon = weaponManager.selectedWeapon;
        //playerWeapon = doubleBarrel;
        playerStats = GetComponent<PlayerStats>();
        GameObject instantiatedWeapon = Instantiate(playerWeapon.weaponPrefab, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform);
        weaponAudioSource = instantiatedWeapon.GetComponent<AudioSource>();





    }
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1.0f / (playerWeapon.fireRate * playerStats.playerAttackSpeed);
            

        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(playerWeapon.projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        Projectile p = proj.GetComponent<Projectile>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * playerWeapon.projectileSpeed;
        }

        if (weaponAudioSource != null && weaponAudioSource.clip != null)
        {
            weaponAudioSource.PlayOneShot(weaponAudioSource.clip);
        }
        else
        {
            Debug.LogWarning("Weapon AudioSource or AudioClip is missing.");
        }

        if (p != null)
        {
            p.weaponData = playerWeapon;
        }
    }

}

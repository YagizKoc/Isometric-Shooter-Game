using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    //Semi-auto, auto ayrımı bilerek yapılmadı. Oyun türünde semi-auto zorlayıcı olur
    public string weaponName;
    public float damage;
    public float fireRate;
    public float range;
    public int magazineSize;
    public float reloadTime;
    public float projectileSpeed;
    public float weaponRecoil;
    public ParticleSystem waeaponParticle;
    public GameObject projectilePrefab;
    public GameObject weaponPrefab;
    
}

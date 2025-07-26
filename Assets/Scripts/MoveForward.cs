using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float spawnPositionZ; 
    private float spawnPositionX;
    private PlayerStats playerStats;
    private WeaponManager weaponManager;
    private WeaponData selectedWeapon;

    void Start()
    {
       
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();

        //selecteing weapon scripable object
        weaponManager = FindFirstObjectByType<WeaponManager>();
        selectedWeapon = weaponManager.selectedWeapon;
        spawnPositionZ = transform.position.z; // Merminin doğduğu pozisyonu kaydeder
        spawnPositionX = transform.position.x;
    }

    void Update()
    {
        // Mermiyi z ekseninde ileriye doğru hareket ettir
        transform.Translate(Vector3.forward * Time.deltaTime * selectedWeapon.projectileSpeed);
        Debug.Log(playerStats.playerRange);
        // merminin X-Z düzlemindeki yer değiştirmesi weapon rangeden fazla olursa mermi yok olur
        if (Mathf.Abs((spawnPositionZ - transform.position.z) * (spawnPositionZ - transform.position.z) + (spawnPositionX - transform.position.x) * (spawnPositionX - transform.position.x)) > (playerStats.playerRange * playerStats.playerRange) + (selectedWeapon.range * selectedWeapon.range))
        {
            Destroy(gameObject);
        }
        
    }
}

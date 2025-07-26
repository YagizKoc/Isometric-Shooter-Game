using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyHP = 10.0f;
    public float enemyAD = 1.0f;
    public float enemyAttackSpeed = 1.0f;
    public float enemyMovementSpeed = 1.0f;
    public float enemyCritChance = 0.0f;
    public float enemyRange = 1.0f;
    public int expYield;
    [SerializeField] GameObject xpCollectible;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] float healthDropChance;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath() {

        //Destroy(enemy);
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
        gameManager.KillCount();
        Instantiate(xpCollectible, new Vector3 (transform.position.x, 0.35f, transform.position.z), transform.rotation);
        if (gameObject.CompareTag("Boss")) {

            GetComponent<BossController>().BossHpBarDisable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Projectile p = other.GetComponent<Projectile>();
            if (p == null || p.weaponData == null) return;

            WeaponData weaponData = p.weaponData;

            Destroy(other.gameObject);

            float baseDamage = weaponData.damage;

            //Crit Roll
            float critRoll = gameManager.Random100();
            if (critRoll <= playerStats.playerCritChance)
            {
                enemyHP -= baseDamage * playerStats.playerCritMultiplier * playerStats.playerAD;
            }
            else
            {
                enemyHP -= baseDamage * playerStats.playerAD;
            }

            //Life leech
            playerStats.playerHP = Mathf.Min(
                playerStats.playerHP + (baseDamage * (playerStats.playerLifeLeech / 100f)),
                playerStats.playerMaxHp
            );
        }
        
        
    
    }

}

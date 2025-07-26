using UnityEngine;
using System.Collections;
using static Enums;

public class PlayerStats : MonoBehaviour
{
    public float playerMaxHp = 100.0f;
    public float playerHP = 100.0f;
    public float playerHealthRegen = 0.01f; //This value  applies every second
    public float playerAD = 1.0f;
    public float playerAttackSpeed = 1.0f;
    public float playerMovementSpeed = 2.0f;
    public float playerCritChance = 0.0f;
    public float playerCritMultiplier = 1.5f;
    public float playerRange = 1.0f;
    public float playerLifeLeech = 0.0f;
    public float mapSize;
    public float pickupRange;
    public int luck;
    public float playerEXP;
    public int playerLevel = 1;
    public float expToLevel = 10.0f;
    private EnemyStats enemyStats;
    public GameObject playerWeapon;
    public GameObject bullet;
    [SerializeField] GameObject upgradeDrop;
    public GameManager gameManager;
    bool canDamage = true;

    private void Start()
    {
        playerEXP = 0.0f;
        playerHP = Mathf.FloorToInt(playerMaxHp);
        playerWeapon = GameObject.FindGameObjectWithTag("weapon");
        StartCoroutine(HealthRegen());
        

    }

   

    void Update()
    {
        if (playerHP <= 0)
        {
            gameManager.GameOver();
            
        }

        if (playerEXP >= expToLevel && UpgradeButton.isUpgradeDone) {

            Instantiate(upgradeDrop, new Vector3(Random.Range(-mapSize, mapSize), 1.5f, Random.Range(-mapSize, mapSize)), transform.rotation);
            playerEXP -= expToLevel;

        }
    }

    
    public void LevelUp() {

        
        playerLevel += 1;
        
        // Expopnantial expToLevel = 5*(playerLevel * (playerLevel -1 )) - (playerLevel * 5);
        expToLevel *= 1.15f;

        //Check Debug Log
        Debug.Log("You leveled up. Your level is: " +  playerLevel);

        //Upgraded Stats
        playerMaxHp += 1;
        playerAD += 0.01f;
        //Renewed HP
        //playerHP = Mathf.FloorToInt(playerMaxHp);

        //Open Upgrade Panel
        gameManager.upgradePanel();
        UpgradeButton.isUpgradeDone = false;

    }

    public void Upgrade(int upgradeAmount) {

        playerMaxHp += upgradeAmount;
    }

    IEnumerator HealthRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (playerHP < playerMaxHp)
                playerHP += playerHealthRegen;
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("enemy") && canDamage)
        {
            EnemyStats localEnemyStats = other.GetComponent<EnemyStats>();
            if (localEnemyStats != null)
            {
                TakeDamage(localEnemyStats.enemyAD);
                StartCoroutine(DamageCooldown());
            }

        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("projectile"))
        {
            MageProjectile projectile = other.GetComponent<MageProjectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.projectileDamage);
                Destroy(other.gameObject);

            }
        }
    }
    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }

    public void TakeDamage(float damage)
    {
        playerHP -= damage;
    }

    public void PlayerWeaponSelect() {

    
    }
}

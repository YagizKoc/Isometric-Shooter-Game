using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    private PlayerStats playerStats;
    public float mapSize = 20f;   
    public float minSpawnDelay = 1f; 
    public float maxSpawnDelay = 5f; 
    public float maxPowerUpDelay = 1f;
    public float minPowerUpDelay = 5f;
    private float time;
    public GameObject[] powerUps;
    public GameManager gameManager;
    [SerializeField] GameObject firstBoss;
    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        gameManager.isGameActive = true;
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnBossAfterTime(300f));
    }

    [SerializeField] float baseSpawnDelay = 4.0f;
    [SerializeField] float difficultyOffset;

    float SpawnAccelerator()
    {
        float elapsed = gameManager.elapsedTime;
        float delay = baseSpawnDelay - Mathf.Log(elapsed + difficultyOffset, 10);
        return Mathf.Max(minSpawnDelay, delay);
    }


    IEnumerator SpawnEnemyRoutine()
    {
        while (gameManager.isGameActive)
        {
            // Bekleme süresi rastgele olsun
            float waitTime = SpawnAccelerator();
            yield return new WaitForSeconds(waitTime);

            while (true)
            {
                // Rastgele pozisyon
                Vector3 spawnPos = new Vector3(
                Random.Range(-playerStats.mapSize, playerStats.mapSize),
                0.5f, // Yer seviyesi
                Random.Range(-playerStats.mapSize, playerStats.mapSize)
            );

            //Is spawn pos distance to player enough ?
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float distanceX = (playerX - spawnPos.x) * (playerX - spawnPos.x);
            float distanceZ = (playerZ - spawnPos.z) * (playerZ - spawnPos.z);
            float distance = distanceX + distanceZ;

                    if (distance >= 25) {
                    // spawn the enemy
                    Vector3 directionToPlayer = player.transform.position - spawnPos; //Direction to player
                    Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                    Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, lookRotation);
                     break;
                    }
            
            
            }
        }
    }

    /*IEnumerator SpawnPowerUp()
    {
        while (gameManager.isGameActive)
        {
            // Bekleme süresi rastgele olsun
            float waitTime = Random.Range(minPowerUpDelay, maxPowerUpDelay);
            yield return new WaitForSeconds(waitTime);

            // Rastgele pozisyon
            Vector3 spawnPos = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0.5f, // Yer seviyesi
                Random.Range(-spawnRange, spawnRange)
            );

            // Düşmanı oluştur
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPos, Quaternion.identity);
        }
    }*/

    IEnumerator SpawnBossAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBoss();
    }

    void SpawnBoss()
    {

        while (true)
        {
            // Rastgele pozisyon
            Vector3 spawnPos = new Vector3(
            Random.Range(-playerStats.mapSize, playerStats.mapSize),
            0.5f, // Yer seviyesi
            Random.Range(-playerStats.mapSize, playerStats.mapSize)
        );

            //Is spawn pos distance to player enough ?
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float distanceX = (playerX - spawnPos.x) * (playerX - spawnPos.x);
            float distanceZ = (playerZ - spawnPos.z) * (playerZ - spawnPos.z);
            float distance = distanceX + distanceZ;

            if (distance >= 25)
            {
                // spawn the enemy
                Vector3 directionToPlayer = player.transform.position - spawnPos; //Direction to player
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                Instantiate(firstBoss, spawnPos, lookRotation);
                break;
            }
        }
    }
}

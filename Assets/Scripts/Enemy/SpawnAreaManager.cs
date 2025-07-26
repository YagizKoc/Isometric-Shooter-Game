using UnityEngine;
using System.Collections;

public class SpawnAreaManager : MonoBehaviour
{
    public GameObject enemy;
    public GameManager gameManager;
    public float spawnRange;
    private Vector3 managerPos;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 5f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        managerPos = transform.position;
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (gameManager.isGameActive)
        {
            // Bekleme süresi rastgele olsun
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);

            // Rastgele pozisyon
            Vector3 spawnPos = new Vector3(
                Random.Range(managerPos.x - spawnRange, managerPos.x + spawnRange),
                0.5f, // Yer seviyesi
                Random.Range(managerPos.z - spawnRange, managerPos.z +spawnRange)
            );

            // Düşmanı oluştur
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
}

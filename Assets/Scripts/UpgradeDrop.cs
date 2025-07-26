using UnityEngine;

public class UpgradeDrop : MonoBehaviour
{
    private PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {

            playerStats.LevelUp();
            Destroy(gameObject);
        }
    }
}

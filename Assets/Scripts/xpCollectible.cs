using UnityEngine;

public class xpCollectible : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float xpAmount;
     BoxCollider col;
     bool moveToPlayer = false;
     PlayerStats playerStats;
     EnemyStats enemyStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        enemyStats = GameObject.FindFirstObjectByType<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        col = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (moveToPlayer)
        {
            float speed = 10f;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        if (transform.position == player.transform.position) {

            Destroy(gameObject);
            playerStats.playerEXP = playerStats.playerEXP + enemyStats.expYield;
        }
        col.size = new Vector3(playerStats.pickupRange, 1, playerStats.pickupRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveToPlayer = true;
        }
    }

}

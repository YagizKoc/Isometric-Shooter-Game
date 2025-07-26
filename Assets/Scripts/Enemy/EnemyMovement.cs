using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed = 1.0f;
    private GameObject player;
    private GameManager gameManager;
    private EnemyStats enemyStats;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindFirstObjectByType<GameManager>();
        enemyStats = FindFirstObjectByType<EnemyStats>();

    }

    void Update()
    {
        if (player == null) return;

        if (gameManager.isGameActive)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            float distance = direction.magnitude;

            if (distance > 0.5f)
            {
                Vector3 moveDir = direction.normalized;
                transform.Translate(moveDir * enemyStats.enemyMovementSpeed * Time.deltaTime, Space.World);
                
            }

            //Rotation
            
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
            }
            
        }
    }


}

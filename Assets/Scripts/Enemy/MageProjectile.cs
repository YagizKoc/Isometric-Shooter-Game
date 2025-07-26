using UnityEngine;

public class MageProjectile : MonoBehaviour
{
    private GameManager gameManager;
    private float spawnPositionZ;
    private float spawnPositionX;
    [SerializeField] private float projectileRange;
    [SerializeField] private float projectileSpeed;
    [SerializeField] public float projectileDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        spawnPositionZ = transform.position.z; // Merminin doğduğu pozisyonu kaydeder
        spawnPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameManager.isGameActive)
        {

            transform.Translate(Vector3.forward *  Time.deltaTime * projectileSpeed, Space.Self);





            // merminin X-Z düzlemindeki yer değiştirmesi weapon rangeden fazla olursa mermi yok olur
            if (Mathf.Abs((spawnPositionZ - transform.position.z) * (spawnPositionZ - transform.position.z) + (spawnPositionX - transform.position.x) * (spawnPositionX - transform.position.x)) > projectileRange * projectileRange)
            {
                Destroy(gameObject);
            }
        }
    }
}

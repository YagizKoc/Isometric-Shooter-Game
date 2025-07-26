using UnityEngine;
using System.Collections;
public class PlayerDetectionControl : MonoBehaviour
{

    private EnemyStats enemyStats;
    private PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyStats = GameObject.FindWithTag("enemy").GetComponent<EnemyStats>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool canDamage = true;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            playerStats.playerHP -= enemyStats.enemyAD;
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }

}

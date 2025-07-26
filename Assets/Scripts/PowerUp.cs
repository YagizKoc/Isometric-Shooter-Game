using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;
    public bool isFire = false;
    public bool isDamage = false;
    public bool isMoveSpeed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Frpowerup"))
        {

            playerStats.playerAttackSpeed *= 2.0f;
        }
        else if (gameObject.CompareTag("Mspowerup")) {

            playerStats.playerMovementSpeed *= 2.0f;
        }
        else if (gameObject.CompareTag("Dmgpowerup"))
        {

            playerStats.playerAD *= 2.0f;
        }

        if (other.CompareTag("Player"))
        {

            Destroy(gameObject);
        }
    }

    
}

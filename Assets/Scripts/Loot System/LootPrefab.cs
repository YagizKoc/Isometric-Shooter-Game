using UnityEngine;

public class LootPrefab : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            // Can ver, altın ver vs.
            Destroy(gameObject);

            //Health Drop
            switch(gameObject.tag){


                case "healthDrop":

                    playerStats.playerHP = Mathf.Min(playerStats.playerHP + 5, playerStats.playerMaxHp);
                    Debug.Log("player healed for 5");
                    break;
                case "xpBundle1":

                    playerStats.playerEXP += 50;
                    Debug.Log("player earned 50 XP");
                    break;
                case "bossXpBundle":

                    playerStats.playerEXP += 100;
                    Debug.Log("player earned 100 XP");
                    break;

            }

            /*
            if (gameObject.CompareTag("healthDrop"))
            {

                playerStats.playerHP = Mathf.Min(playerStats.playerHP + 5, playerStats.playerMaxHp);
                Debug.Log("player healed for 5");
            }
            else if (gameObject.CompareTag("xpBundle1"))
            {

                playerStats.playerEXP += 50;
                Debug.Log("player earned 50 XP");
            }*/
        }
        
    }

}

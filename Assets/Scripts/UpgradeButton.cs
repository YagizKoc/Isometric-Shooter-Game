using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using static Enums;

public class UpgradeButton : MonoBehaviour
{
    private PlayerStats playerStats;
    private GameManager gameManager;
    public static bool isUpgradeDone = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        gameManager = FindFirstObjectByType<GameManager>();

        GetComponent<Button>().onClick.AddListener(ApplyUpgrade);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyUpgrade()
    {

        switch (gameObject.tag)
        {

            case "healthUpgrade":
                playerStats.playerMaxHp += 5;
                break;
            case "healthRegenUpgrade":
                playerStats.playerHealthRegen += 0.05f;
                break;
            case "attackSpeedUpgrade":
                playerStats.playerAttackSpeed *= 1.1f;
                break;
            case "critChanceUpgrade":
                playerStats.playerCritChance += 0.03f;
                break;
            case "moveSpeedUpgrade":
                playerStats.playerMovementSpeed *= 1.05f;
                break;
            case "rangeUpgrade":
                playerStats.playerRange *= 1.15f;
                break;
            case "lifeLeechUpgrade":
                playerStats.playerLifeLeech += 1.0f;
                break;
            case "attackDamageUpgrade":
                playerStats.playerAD *= 1.1f;
                break;
            case "mapSizeUpgrade":
                playerStats.mapSize *= 1.1f;
                break;
            case "critMultiplierUpgrade":
                playerStats.playerCritMultiplier += 15.0f;
                break;
            case "pickupRangeUpgrade":
                playerStats.pickupRange *= 1.25f;
                break;
        }

        // Upgrade Panel'deki tüm butonları yok et
        gameManager.DestroyAllChildren(gameManager.upgradePanelObject);
        isUpgradeDone = true;
        Time.timeScale = 1.0f;
    }

    
    void DestroyAllWithTag(string tagName)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagName);

        foreach (GameObject obj in taggedObjects)
        {
            Destroy(obj);
        }
    }

}

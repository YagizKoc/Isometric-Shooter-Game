using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using static Enums;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemySpawner;
    public GameObject player;

    public PlayerStats playerStats;

    public TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI bossHp;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeText;
    public Transform uiParent;
    //Buttons
    [SerializeField] GameObject mainMenu;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject startButton;
    [SerializeField] GameObject pauseMenu;
    private Button[] resumeButtons;
    [SerializeField] GameObject settingsMenu;
    public StatsPanelController statsPanelController;
    [SerializeField] GameObject statsPanel;
    [SerializeField] GameObject weaponsMenu;
    public GameObject upgradePanelObject;
    public GameObject[] upgradeButtons;
    public Button upgradeButton;

    public bool isGameActive;
    private bool paused = false;
    public int waveNumber = 0;
    public int killCount;
    private float startTime;
    public int elapsedTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameActive = true;
        startTime = Time.time;
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in GameManager!");
            return;
        }

        playerStats = player.GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found on Player object!");
            return;
        }

        UpdateHpBar();
        
        resumeButtons = pauseMenu.GetComponentsInChildren<Button>();
        foreach (Button btn in resumeButtons)
        {
            switch (btn.name)
            {
                case "Resume Button":
                    btn.onClick.AddListener(TogglePauseMenu);
                    break;
                case "Stats Button":
                    btn.onClick.AddListener(ShowStatsPanel);
                    break;
                case "Settings Button":
                    btn.onClick.AddListener(ShowSettingsMenu);
                    break;
                case "Main Menu Button":
                    btn.onClick.AddListener(MainMenuButton);
                    break;
                case "Exit Button":
                    btn.onClick.AddListener(QuitGame);
                    break;
            }
        }

        /*//For WebGlbuild delete this after you conculde the problem
        if (playerStats.playerWeapon != null) {

            weaponsMenu.gameObject.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
        TimeMesuring();

        UpdateHpBar();
        UpdateExpBar();
        if (!isGameActive) { 

            enemySpawner.gameObject.SetActive(false);
            
        }
        if (isGameActive) {

            //Pause Menu Controls
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (settingsMenu.activeSelf)
                {
                    settingsMenu.SetActive(false);
                    pauseMenu.SetActive(true);
                }
                else if (statsPanel.activeSelf)
                {
                    statsPanel.SetActive(false);
                    pauseMenu.SetActive(true);
                }
                else if (pauseMenu.activeSelf)
                {
                    TogglePauseMenu(); // Pause'dan çık
                }
                else
                {
                    // Eğer oyun aktifken ESC'e basılmışsa pause'a gir
                    TogglePauseMenu();
                }
            }
        }
    }

    

    public void GameOver() { 
    
        isGameActive = false;
        playerStats.playerHP = 0;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);

    }

    public void UpdateHpBar()
    {
        if (playerStats != null) {

            hpText.text = "HP: " + Mathf.FloorToInt(playerStats.playerHP) + " /" + Mathf.FloorToInt(playerStats.playerMaxHp);
        }
        
    }

    public void UpdateExpBar()
    {
        if (playerStats != null) {

            expText.text = "Exp: " + Mathf.FloorToInt(playerStats.playerEXP) + " /" + Mathf.FloorToInt(playerStats.expToLevel);
        }
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void MainMenuButton() {

        if (Time.timeScale == 0){

            Time.timeScale = 1;
        }
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");
        

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ShowSettingsMenu() { 

        settingsMenu.gameObject.SetActive(true);
    }
    public void upgradePanel()
    {
        RectTransform canvasRect = uiParent.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        float width = canvasRect.rect.width;

        float offset = width / 4f; // 1/4 oranla sola/sağa yayılma

        Vector2[] positions = new Vector2[3];
        positions[0] = new Vector2(-offset, 0);  // Sol
        positions[1] = new Vector2(0f, 0);       // Orta
        positions[2] = new Vector2(offset, 0);   // Sağ

        for (int i = 0; i < 3; i++)
        {
            GameObject button = Instantiate(upgradeButtons[Random.Range(0, upgradeButtons.Length)], uiParent);

            RectTransform rt = button.GetComponent<RectTransform>();
            rt.anchoredPosition = positions[i];

        }
        Time.timeScale = 0.0f;
    }
    public void KillCount() {

        killCount += 1;
    }

    public void DestroyAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void TogglePauseMenu() {

        
        if (paused)
        {
            Time.timeScale = 1.0f;
            pauseMenu.gameObject.SetActive(false);
            paused = !paused;
        }
        else if (!paused) {

            Time.timeScale = 0.0f;
            pauseMenu.gameObject.SetActive(true);
            paused = !paused;
        }
        
    }

    void TimeMesuring()
    {
        //Meseruing Time
        elapsedTime = Mathf.FloorToInt(Time.time - startTime);
        int hours = elapsedTime / 3600;
        int minutes = (elapsedTime % 3600) / 60;
        int seconds = elapsedTime % 60;
        timeText.text = $"Time: {hours:D2}:{minutes:D2}:{seconds:D2}";
    }

    public void ShowStatsPanel() { 
    
        statsPanel.gameObject.SetActive(true);
        statsPanelController.UpdateStat("Level", Mathf.FloorToInt(playerStats.playerLevel));
        statsPanelController.UpdateStat("Health", Mathf.FloorToInt(playerStats.playerHP));
        statsPanelController.UpdateStat("Health Regen", playerStats.playerHealthRegen);
        statsPanelController.UpdateStat("Attack Damage", playerStats.playerAD);
        statsPanelController.UpdateStat("Attack Speed", playerStats.playerAttackSpeed);
        statsPanelController.UpdateStat("Critical Chance", playerStats.playerCritChance);
        statsPanelController.UpdateStat("Movement Speed", playerStats.playerMovementSpeed);
        statsPanelController.UpdateStat("Range", playerStats.playerRange);
        statsPanelController.UpdateStat("Life Leech", playerStats.playerLifeLeech);
    }

    public void OpenWeaponsMenu() {

        weaponsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    void HideStatsPanel() {

        statsPanel.gameObject.SetActive(false);
    }

    public float Random100() {

        return Random.Range(0f, 100f);
    }
}

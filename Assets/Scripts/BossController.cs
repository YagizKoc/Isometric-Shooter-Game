using UnityEngine;
using TMPro;
public class BossController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bossHp;
    [SerializeField] int bossMaxHp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHp = GameObject.Find("Boss HP").GetComponent<TextMeshProUGUI>();
        bossMaxHp = Mathf.FloorToInt(gameObject.GetComponent<EnemyStats>().enemyHP);
    }

    // Update is called once per frame
    void Update()
    {
        bossHp.text = Mathf.FloorToInt(gameObject.GetComponent<EnemyStats>().enemyHP).ToString() + "/" + bossMaxHp;
    }

    public void BossHpBarEnable() {

        bossHp.gameObject.SetActive(true);
    }

    public void BossHpBarDisable()
    {

        bossHp.gameObject.SetActive(false);
    }
}

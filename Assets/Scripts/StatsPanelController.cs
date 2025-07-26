using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanelController : MonoBehaviour
{
    public GameObject statsPanel;
    private Dictionary<string, TextMeshProUGUI> statTexts;

    void Awake()
    {
        statTexts = new Dictionary<string, TextMeshProUGUI>();
        var texts = statsPanel.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var t in texts)
        {
            statTexts[t.name] = t;
        }
    }
    private string FormatStatName(string rawName)
    {
        switch (rawName)
        {
            case "HealthText": return "Health";
            case "DamageText": return "Damage";
            case "RegenText": return "Health Regen";
            case "SpeedText": return "Move Speed";
            case "FireRateText": return "Attack Speed";
            case "CritText": return "Crit Chance";
            case "LeechText": return "Life Leech";
            default: return rawName;
        }
    }
    public void UpdateStat(string statName, float value)
    {
        if (statTexts.ContainsKey(statName))
        {
            statTexts[statName].text = $"{FormatStatName(statName)}: {value:F2}";
        }
        else
        {
            Debug.LogWarning("Stat text not found: " + statName);
        }
    }
}

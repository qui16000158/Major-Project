using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// QUI16000158 | James Quinney
public class DisplayStats : MonoBehaviour
{
    [SerializeField]
    TMP_Text displayText;

    // This will fill in a text box with the player's stats
    public void UpdateDisplay()
    {
        string currentText = "";

        Stats stats = PlayerLevelManager.instance.stats;

        // Level Display
        currentText += "Level: " + stats.level + "\n";

        // Mathf.FloorToInt is used to negate the effects of floating point precision loss

        // EXP Display
        currentText += "EXP: " + Mathf.FloorToInt(stats.EXP) + "/" + Mathf.FloorToInt(stats.LevelFromEXP) + "\n";

        currentText += "\n"; // Separate level/exp from health/stamina

        // Health display
        currentText += "Health: " + Mathf.FloorToInt(stats.MaxHealth) + "\n";

        // Stamina display
        currentText += "Stamina: " + Mathf.FloorToInt(stats.MaxStamina);

        displayText.text = currentText;
    }
}

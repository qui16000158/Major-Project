using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        // EXP Display
        currentText += "EXP: " + stats.EXP + "/" + stats.LevelFromEXP + "\n";

        currentText += "\n"; // Separate level/exp from health/stamina

        // Mathf.FloorToInt is used to negate the effects of floating point precision loss

        // Health display
        currentText += "Health: " + Mathf.FloorToInt(stats.MaxHealth) + "\n";

        // Stamina display
        currentText += "Stamina: " + Mathf.FloorToInt(stats.MaxStamina);

        displayText.text = currentText;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    PlayerStats playerStatsScript;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        playerStatsScript = FindFirstObjectByType<PlayerStats>().GetComponent<PlayerStats>();
        scoreText.text = ": " + playerStatsScript.GetHealth() + "x";
    }
    public void UpdateText()
    {
        scoreText.text = ": " + playerStatsScript.GetScore() + "x";
    }

    public void UpdateHealth()
    {
        healthSlider.value = playerStatsScript.GetHealth();
    }
}

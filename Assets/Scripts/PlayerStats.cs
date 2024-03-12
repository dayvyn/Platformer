using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    int health;
    int score;
    PlayerController playerControllerScript;
    Display displayScript;
    Coin[] coins;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 5;
        displayScript = FindAnyObjectByType<Display>().GetComponent<Display>();
        playerControllerScript = FindAnyObjectByType<PlayerController>().GetComponent<PlayerController>();
        coins = FindObjectsOfType<Coin>();
    }
    public void DecreaseHealth()
    {
        health -= 1;
        displayScript.UpdateHealth();
        if (health == 0)
        {
            SceneManager.LoadScene("DefeatScene");
        }
    }
    public void IncreaseScore()
    {
        score++;
        displayScript.UpdateText();
        if (coins.Length == score)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetScore()
    {
        return score;
    }

}

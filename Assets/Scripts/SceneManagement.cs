using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement: MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    public void GameOver()
    {
        SceneManager.LoadScene("DefeatScene");
    }
}

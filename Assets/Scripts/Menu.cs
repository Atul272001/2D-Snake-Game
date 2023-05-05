using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject gameOver;
    public GameObject playGame;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
        playGame.SetActive(true);
        gameOver.SetActive(false);
    }
    
    public void PlayGame()
    {
        playGame.SetActive(false);
        Time.timeScale = 1;
        gameOver.SetActive(false);
    }
}

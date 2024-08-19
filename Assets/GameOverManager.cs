using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using TMPro;

public class GameOverPanelManager : MonoBehaviour
{
    // Note: To do a gameover, run ScoreManager.GameOver() as this will be deactivated.
    
    public GameObject scoreManager;
    public int score;
    public int highScore;
    public GameObject scoreText;
    public GameObject highScoreText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void updateText()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
        highScoreText.GetComponent<TextMeshProUGUI>().text = highScore.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        scoreManager.GetComponent<ScoreManager>().ScoreReset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.FindGameObjectWithTag("ship").GetComponent<ShipController>(); // I don't know why, but the ship is really slow after restart if this is not called.
    }
}

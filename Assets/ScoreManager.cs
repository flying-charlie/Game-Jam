using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject m_gameOverPanel;
    public GameObject scoreText;
    public GameObject highScoreText;
    int score = 0;
    int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        if (this.score > highScore)
        {
            highScore = this.score;
        }
        updateText();
    }

    void updateText()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
        highScoreText.GetComponent<TextMeshProUGUI>().text = highScore.ToString();
    }

    public void ScoreReset()
    {
        score = 0;
        updateText();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        m_gameOverPanel.SetActive(true);
        m_gameOverPanel.GetComponent<GameOverPanelManager>().score = score;
        m_gameOverPanel.GetComponent<GameOverPanelManager>().highScore = highScore;
        m_gameOverPanel.GetComponent<GameOverPanelManager>().updateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

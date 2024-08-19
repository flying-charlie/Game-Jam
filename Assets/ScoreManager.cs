using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject highScoreText;
    int score = 0;
    int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
        highScoreText.GetComponent<TextMeshProUGUI>().text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

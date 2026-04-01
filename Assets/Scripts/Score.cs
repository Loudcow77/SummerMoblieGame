using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score;

    private void Start()
    {
        // Access the score text and set it
        scoreText.text = score.ToString();
    }

    public void increaseScore()
    {
        // Increase the score when a block is destroyed and update the text
        score++;
        scoreText.text = score.ToString();
    }
}

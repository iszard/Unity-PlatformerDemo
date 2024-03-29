﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // access to the text element that shows the score value
    public Text scoreValue;

    // access to high score value
    public Text highScoreValue;

    // Start is called before the first frame update
    void Start()
    {
        // set the text of our score value
        scoreValue.text = GameManager.instance.score.ToString();

        // set the text of our high score value
        highScoreValue.text = GameManager.instance.highScore.ToString();
    }

    // it will send the player to level 1
    public void RestartGame() {
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("Level1");
    }
}

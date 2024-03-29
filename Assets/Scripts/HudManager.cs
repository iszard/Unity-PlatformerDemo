﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    // score text label
    public Text scoreLabel;
    
    // Start is called before the first frame update
    void Start() {
        // start with corret score
        ResetHud();
    }

    // Show up to date stats of the player
    public void ResetHud() {
        scoreLabel.text = "Score: " + GameManager.instance.score;
    }
}

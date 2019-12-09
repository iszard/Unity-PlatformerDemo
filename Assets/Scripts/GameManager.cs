using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // score of player
    public int score = 0;
    
    // highscore of player
    public int highScore = 0;

    // current level
    public int currentLevel = 1;

    // how many levels there are
    public int highestLevel = 2;

    // HUD manager
    HudManager hudManager;

    // static instance of the GM can be accessed from anywhere
    public static GameManager instance;

    void Awake() {
        // check that it exists
        if(instance==null) {
            // assign it to the current Object
            instance = this;
        }

        // make sure that it is queal to the current object
        else if(instance != this) {
            // find an Object of type HudManager
            instance.hudManager = FindObjectOfType<HudManager>();

            // destroy the current game object - we only need 1 and we already have it.
            Destroy(gameObject);
        }

        // don't destroy this object when changing scanes!
        DontDestroyOnLoad(gameObject);

        // find an Object of type HudManager
        hudManager = FindObjectOfType<HudManager>();
    }

    public void IncreaseScore(int amount) {
        // increase score by the amount
        score += amount;

        // Update HUD
        if(hudManager != null)
        hudManager.ResetHud();

        // have we surpassed out high score
        if(score > highScore) {
            // set a new highscore
            highScore = score;

            print("New Record! " + score);
        } else {
            // show the new score
            print("New Score: " + score);
        }
    }

    // Reset the game
    public void ResetGame() {
        // reset our score
        score = 0;

        // Update HUD
        if(hudManager != null)
        hudManager.ResetHud();

        // set the current level to 1
        currentLevel = 1;

        // load the current level scene
        SceneManager.LoadScene("Level1");
    }

    // Send the player to the next level
    public void IncreaseLevel() {
        // check if there are more levels
        if(currentLevel < highestLevel) {
            currentLevel += 1;
        } else {
            // we are going back to level 1
            currentLevel = 1;
        }

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
}

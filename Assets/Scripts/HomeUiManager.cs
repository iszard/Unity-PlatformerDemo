using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUiManager : MonoBehaviour {

    // start at level 1
    public void StartGame() {
        SceneManager.LoadScene("Level1");
    }
}

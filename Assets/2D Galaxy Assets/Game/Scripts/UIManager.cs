using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image livesImageDisplay;
    public int score;
    public Text scoreText;
    public GameObject titleScreen;
    private GameManager _gameManager;

    public void updateLives(int currentLives) {
        livesImageDisplay.sprite = lives[currentLives]; 
    }

    public void UpdateScore() {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void decreaseScore() {
        score -= 10;
        scoreText.text = "Score: " + score;
        if (score == -50) {
           
            //SceneManager.UnloadSceneAsync("Single_Player");
            SceneManager.LoadScene("First_Boss");
            //Time.timeScale = 0;
        }
    }

    public void HideTitleScreen() {
        titleScreen.SetActive(false);
    }

    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("Main_Menu");
    }

    public void ShowTitleScreen() {
        titleScreen.SetActive(true);  
    }

    public void ClearScore() {
        score = 0;
        scoreText.text = "Score: " + score;
    }
       


}

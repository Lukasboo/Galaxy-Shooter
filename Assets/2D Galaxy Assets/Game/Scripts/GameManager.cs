using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    public bool isCoopMode = false;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject coopPlayers;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private GameManager _gameManager;

    [SerializeField]
    public GameObject _pauseMenuPanel;

    private void Start() {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if (gameOver == true) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Debug.Log("StartSpawnRoutines");

                if (_gameManager.isCoopMode == false) {
                    Instantiate(player, Vector3.zero, Quaternion.identity);
                } else {
                    Instantiate(coopPlayers, Vector3.zero, Quaternion.identity);
                }
                gameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.StartSpawnRoutines();
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene("Main_Menu");
            } 
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }


    }

    public void PauseGame()
    {
        _pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame() {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }



}

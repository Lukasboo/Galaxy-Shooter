using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    private GameManager _gameManager;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject[] powerups;

    // Use this for initialization
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
        
	}

    public void StartSpawnRoutines() {
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    public IEnumerator enemySpawnRoutine() {
        while (_gameManager.gameOver == false) {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

    public IEnumerator PowerupSpawnRoutine() {
        while (_gameManager.gameOver == false) {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    } 

		
}

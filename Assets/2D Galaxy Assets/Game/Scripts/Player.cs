using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool canTripleShoot = false;
	public bool isSpeedBoostActive = false;
    public bool isShieldsActive = false;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    
    [SerializeField]
    private GameObject _playerPrefab;
	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _tripleShootPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameManager _gameManager;

	//fireRate 0.25f
	[SerializeField]
	private float _fireRate = 0.25f;
	private float _canFire = 0.0f;

	[SerializeField]	
	private float speed = 5.0f;

    private UIManager _uimanager;

	public float horizontalInput;  	

    public int lives = 3;
    private SpawnManager _spawnManager;

    private AudioSource _audioSource;

    [SerializeField]
    private GameObject[] _engines;

    private int hitCount = 0;


    // Use this for initialization
    private void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uimanager != null)
        {
            _uimanager.updateLives(lives);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        /*if (_spawnManager != null) {
            _spawnManager.StartSpawnRoutines();
        }*/

        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;

        if (_gameManager.isCoopMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        /*else {
            transform.position = new Vector3(15, 0, 0);
            transform.position = new Vector3(-15, 0, 0);
        }*/

	}
	
	// Update is called once per frame
	private void Update () {
        if (isPlayerOne == true) {
            Movement();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) && isPlayerOne == true) {        
                Shoot();
            }
        }

        if (isPlayerTwo == true)
        {
            PlayerTwoMovement();
            if (Input.GetKeyDown(KeyCode.Keypad0)){
                Debug.Log("shooting enter");
                ShootPlayerTwo();
                //Shoot();
            }
        }

        /*if (Input.GetKeyDown(KeyCode.P)) {
            _uimanager.PauseGame();
            Time.timeScale = 0;
        }*/

	}

	private void Shoot() {
        Debug.Log("Shooooooting");
		if (Time.time > _canFire) {
            _audioSource.Play();
			if (canTripleShoot == true) {
				Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
			} else {
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);			
			}	
		}	
			_canFire = Time.time + _fireRate;						
		
	}

    private void ShootPlayerTwo()
    {
        if (Time.time > _canFire)
        {
            //_audioSource.Play();
            if (canTripleShoot == true)
            {
                Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
        }
        _canFire = Time.time + _fireRate;

    }

	private void Movement() {
		horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		
		if (transform.position.y > 0) { //top
			transform.position = new Vector3(transform.position.x, 0, 0);
		} else if (transform.position.y < -4.2f) {
			transform.position = new Vector3(transform.position.x, -4.2f, 0);
		}

		if (transform.position.x > 9.5) {
			transform.position = new Vector3(-9.5f, transform.position.y, 0);
		} else if (transform.position.x < -9.5) {
			transform.position = new Vector3(9.5f, transform.position.y, 0);
		}

		if (isSpeedBoostActive == true) {
			transform.Translate(Vector3.right * Time.deltaTime * speed * 1.5f * horizontalInput);		
			transform.Translate(Vector3.up * speed * verticalInput * 1.5f * Time.deltaTime);
		} else {
			transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);		
			transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
		}

	}
    //teste a
    private void PlayerTwoMovement()
    {       

        if (transform.position.y > 0)
        { //top
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }

        if (isSpeedBoostActive == true)
        {
            if (Input.GetKey(KeyCode.Keypad8)) {
                transform.Translate(Vector3.up * speed * 1.5f * Time.deltaTime);
            } 

            if (Input.GetKey(KeyCode.Keypad6)) {
                transform.Translate(Vector3.right * speed * 1.5f * Time.deltaTime);
            } 

            if (Input.GetKey(KeyCode.Keypad2)) {
                transform.Translate(Vector3.down * speed * 1.5f * Time.deltaTime);
            } 

            if (Input.GetKey(KeyCode.Keypad4)) {
                transform.Translate(Vector3.left * speed * 1.5f * Time.deltaTime);
            } 

        } else {
            if (Input.GetKey(KeyCode.Keypad8)) {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            } 

            if (Input.GetKey(KeyCode.Keypad6)) {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            } 

            if (Input.GetKey(KeyCode.Keypad2)) {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            } 

            if (Input.GetKey(KeyCode.Keypad4)) {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            } 
        }

    }

    public void Damage() {
        if (isShieldsActive == true) {
            isShieldsActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }
        lives--;
        hitCount++;

        if (hitCount == 1) {
            _engines[0].SetActive(true);
        } else if (hitCount == 2) {
            _engines[1].SetActive(true);
        }

        _uimanager.updateLives(lives);

        if (lives < 1)
        {
            _gameManager.gameOver = true;
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _uimanager.ShowTitleScreen();
            _uimanager.ClearScore();
        }
    }

	public void TripleShotPowerupOn() {
		canTripleShoot = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	public IEnumerator TripleShotPowerDownRoutine() {
		yield return new WaitForSeconds(5.0f);
		canTripleShoot = false;
	}

	public void SpeedUpPowerupOn() {
		isSpeedBoostActive = true;
		StartCoroutine(SpeedUpPowerDownRoutine());
	}

	public IEnumerator SpeedUpPowerDownRoutine() {
		yield return new WaitForSeconds(5.0f);
		isSpeedBoostActive = false;		
	}

    public void ActivateShields() {
        Debug.Log("Ativando escudos - SpeedUpPowerupOn");
        isShieldsActive = true;
        _shieldPrefab.SetActive(true);
    }

}

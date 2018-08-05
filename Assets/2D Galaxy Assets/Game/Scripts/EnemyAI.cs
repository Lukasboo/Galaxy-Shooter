using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	//variable for speed
	[SerializeField]
	private float _speed = 5.0f;

    [SerializeField]
    private GameObject _EnemyExplosiontPrefab;

	
    private UIManager _uimanager;
    [SerializeField]
    private AudioClip _audioClip;

	void Start () {		      
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();      
	}
	
	// Update is called once per frame
	void Update () {	
		transform.Translate(Vector3.down * _speed * Time.deltaTime);						
		if (transform.position.y < -6.5f) {
            createEnemy();
            _uimanager.decreaseScore();
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser") {
            if (other.transform.parent != null) {
                Destroy(other.transform.parent);
            }
            Destroy(other.gameObject);
            Instantiate(_EnemyExplosiontPrefab, transform.position, Quaternion.identity);
            _uimanager.UpdateScore();
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);


        } else if (other.tag == "Player") {
            Debug.Log("coliding com inimigo");
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            Instantiate(_EnemyExplosiontPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);

        }  		
	}

    public void createEnemy() {
        float randomX = Random.Range(-8.28f, 8.28f);
        transform.position = new Vector3(randomX, 6.40f, 0);
    }

}

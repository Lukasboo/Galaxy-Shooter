using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 5.0f;

    private float randomX = 0f;
    private bool toLeft = false;

    [SerializeField]
    private GameObject _bossLaserPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpeedUpPowerDownRoutine());

	}
	
	// Update is called once per frame
	void Update () {
        if (toLeft == true){
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);   
        }

        if (transform.position.x > 7.32f){
            toLeft = true;
        } else if (transform.position.x < -7.32f) {
            toLeft = false;
        }

        //randomX = Random.Range(-8.28f, 8.28f);
        /*if (transform.position.x == 9.5) {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        } else if (transform.position.x < -9.5) {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }*/




        //transform.Translate(Vector3.right * _speed * Time.deltaTime);
        /*if (transform.position.y == -6.5f) {
            randomX = Random.Range(-8.28f, 8.28f);
            transform.position = new Vector3(randomX, 6.40f, 0);
        }*/
        
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent);
            }
            Destroy(other.gameObject);
            /*Instantiate(_EnemyExplosiontPrefab, transform.position, Quaternion.identity);
            _uimanager.UpdateScore();
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);*/


        }
       
    }

    public IEnumerator SpeedUpPowerDownRoutine()
    {
        Debug.Log("contando laser do boss");
        yield return new WaitForSeconds(3.0f);
        Instantiate(_bossLaserPrefab, transform.position, Quaternion.identity);
    }


}

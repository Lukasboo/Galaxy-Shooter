using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour {

    [SerializeField]
    private float speed = 12.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y >= 6.4)
        {
            if (transform.parent != null)
            {
                Destroy(transform.gameObject);
            }
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Debug.Log("coliding com inimigo");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            /*Instantiate(_EnemyExplosiontPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);*/
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID; //0 = triple shot 1 = speed boost, 2 = shields
	
    [SerializeField]
    private AudioClip _audioClip;

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7) {
            Destroy(this.gameObject);
        }
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Colidiu com " + other.name);
		//acecess the player
        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
		if (other.tag == "Player") {
			Player player = other.GetComponent<Player>();
			if (player != null) {
				if (powerupID == 0) {
					player.TripleShotPowerupOn();
				} else if (powerupID == 1) {
					player.SpeedUpPowerupOn();
				} else if (powerupID == 2) {
                    player.ActivateShields();
				}
             
			}

			Destroy(this.gameObject);
		}
	}

}

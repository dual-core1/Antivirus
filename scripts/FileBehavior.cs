using UnityEngine;
using System.Collections;

public class FileBehavior : MonoBehaviour {

	public int type; // 1 = normal, 2 = infected, 3 = malware
	public float MoveSpeed = 9f;
	public GameObject projectile;
	GameObject player;
	bool fired;
	bool dead;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();

		dead = false;
		fired = false;

		switch (type) {
		case 1:
			anim.Play ("file_normal");
			break;
		case 2:
			anim.Play ("file_infected");
			break;
		case 3:
			anim.Play ("file_malware");
			break;
		}

		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			transform.Translate(new Vector3 (MoveSpeed * -1f, 0f, 0f));
		} else {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		bool facing = false;

		// collide with player?
		if (collision.gameObject.CompareTag ("Player")) {

			// player is attacking?
			if (collision.gameObject.GetComponent<PlayerController> ().attacking) {

				// effective frame?
				if (collision.gameObject.GetComponent<PlayerController> ().attackEffective) {

					// check if player is facing this gameobject
					if (collision.gameObject.transform.position.x <= this.transform.position.x) { // to left of object
						if (collision.gameObject.transform.localScale.x > 0) { // player facing right
							facing = true;
						}
					}

					if (collision.gameObject.transform.position.x >= this.transform.position.x) { // to right of object
						if (collision.gameObject.transform.localScale.x < 0) { // player facing left
							facing = true;
						}
					}

					// if facing, this file has been killed
					if (facing) {

						// consequences
						switch (type) {
							// if killed a normal file, set player's score to negative 1, ending the game
						case 1:
							collision.gameObject.GetComponent<PlayerController> ().SetScore(-1);
							break;
							// if killed malware, increase player score
						case 2:
							collision.gameObject.GetComponent<PlayerController> ().AddScore(50);
							break;
						}

						GameObject.Find ("kill sound").GetComponent<AudioSource> ().Play (); // play kill sound

						Destroy (gameObject);
					}
				}
			}

			// player is healing?
			if (collision.gameObject.GetComponent<PlayerController> ().healing) {

				// effective frame?
				if (collision.gameObject.GetComponent<PlayerController> ().healEffective) {

					// check if player is facing this gameobject
					if (collision.gameObject.transform.position.x <= this.transform.position.x) { // to left of object
						if (collision.gameObject.transform.localScale.x > 0) { // player facing right
							facing = true;
						}
					}
					
					if (collision.gameObject.transform.position.x >= this.transform.position.x) { // to right of object
						if (collision.gameObject.transform.localScale.x < 0) { // player facing left
							facing = true;
						}
					}

					// if infected, heal
					if (facing && type == 2) {
						GameObject.Find ("heal sound").GetComponent<AudioSource> ().Play (); // play heal sound
						type = 1;
						anim.Play ("file_transition");
					}
				}
			}
		}

		// collide with boundary at left side of game world?
		if (collision.gameObject.CompareTag ("Boundary")) {
			// consequences
			switch (type) {
			
			// normal file? +100 points.
			case 1:
				player.GetComponent<PlayerController> ().AddScore (100);
				break;
			// infected file? -100 points.
			case 2:
				player.GetComponent<PlayerController> ().AddScore (-100);
				break;
			// malware file? -200 points.
			case 3:
				player.GetComponent<PlayerController> ().AddScore (-200);
				break;
			}
			Destroy (gameObject);
		}

		// collision with the fire threshold?
		if (collision.gameObject.CompareTag ("Fire")) {
			// is malware?
			if (type == 3) {
				// has not already fired?
				if (!fired) {
					// get player's position
					Vector3 dirToPlayer = player.transform.position - transform.position;

					// instantiate projectile
					GameObject proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
					proj.GetComponent<ProjectileBehavior> ().Trajectory = dirToPlayer.normalized;

					// play shoot sound
					GetComponent<AudioSource> ().Play ();
				}
			}
		}
	}
}

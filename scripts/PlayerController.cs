using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float MoveSpeed = 10f;

	float Health;
	int score;
	int stage;

	Animator anim;
	Rigidbody2D rb;

	float MoveDirX; // 0 = none, 1 = right, -1 = left
	float MoveDirY; // 0 = none, -1 = down, 1 = up
	bool attacking;
	bool healing;

	bool dead;

	// effective frames of attack and heal anims
	bool attackEffective;
	bool healEffective;

	void Start () {
		Health = 3;
		score = 0;
		stage = 1;
		MoveDirX = 0f;
		MoveDirY = 0f;
		anim = GetComponent<Animator> ();
		attacking = false;
		healing = false;
		rb = GetComponent<Rigidbody2D> ();
		attackEffective = false;
		healEffective = false;
	}

	void Update () {

		if (Health < 1) {
			dead = true;
			if (Health < 0)
				Health = 0;
		}

		MoveDirX = 0f;
		MoveDirY = 0f;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.localScale = new Vector3(-2, 2, 1);

			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("player_walk")) {
				anim.Play ("player_walk");
			}
			MoveDirX = -1f;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.localScale = new Vector3(2, 2, 1);

			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("player_walk")) {
				anim.Play ("player_walk");
			}
			MoveDirX = 1f;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("player_walk")) {
				anim.Play ("player_walk");
			}
			MoveDirY = 1f;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("player_walk")) {
				anim.Play ("player_walk");
			}
			MoveDirY = -1f;
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("player_attack")) {
				attacking = true;
				attackEffective = false; // should start out false
				anim.Play ("player_attack");
			}
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("player_heal")) {
				healing = true;
				healEffective = false; // should start out false
				anim.Play ("player_heal");
			}
		}

		if (MoveDirX == 0 && MoveDirY == 0 && !attacking && !healing) {
			anim.Play("player_idle");
		}

		if (dead) {
			MoveDirX = 0;
			MoveDirY = 0;
			anim.Play ("player_dead");
		}

		transform.Translate(MoveDirX * MoveSpeed * Time.deltaTime, MoveDirY * MoveSpeed * Time.deltaTime, 0f);
	}

	// this is triggered when the healing anim finishes, so it should set the healing value to false
	void HealingAnimDone () {
		healing = false;
		HealingAnimSetIneffective ();
	}

	// this is triggered when the attack anim finishes, so it should set the attacking value to false
	void AttackingAnimDone () {
		attacking = false;
	}

	// this is triggered on the healing anim's effective frame
	void HealingAnimSetEffective () {
		healEffective = true;
	}

	// this is triggered on the attack anim's effective frame
	void AttackAnimSetEffective () {
		attackEffective = true;
	}

	// this is triggered after the healing anim's effective frame
	void HealingAnimSetIneffective () {
		healEffective = false;
	}

	// this is triggered after the attack anim's effective frame
	void AttackAnimSetIneffective () {
		attackEffective = false;
	}

	// make sure the player can't move past the boundary
	void OnCollisionEnter(Collision collision) {
		// this is some yandev type shit right here

		if (collision.gameObject.CompareTag ("Boundary")) {
			rb.AddForce (new Vector2 (2f, 0f));
		} else if (collision.gameObject.CompareTag ("UBound")) {
			rb.AddForce (new Vector2 (0f, 2f));
		} else if (collision.gameObject.CompareTag ("LBound")) {
			rb.AddForce (new Vector2 (0f, -2f));
		} else if (collision.gameObject.CompareTag ("RBound")) {
			rb.AddForce (new Vector2 (-2f, 0f));
		} else if (collision.gameObject.CompareTag ("Projectile")) {
			Health -= 1;
		}
	}


}

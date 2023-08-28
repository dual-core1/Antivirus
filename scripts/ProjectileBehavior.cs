using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour {

	public float Speed = 5;
	public float DirX;
	public float DirY;

	void Start () {
	
	}

	void Update () {
		transform.Translate (DirX * Speed * Time.deltaTime, DirY * Speed * Time.deltaTime, 0f);
	}

	void OnCollisionEnter (Collision collision) {
		Destroy (gameObject);
	}
}

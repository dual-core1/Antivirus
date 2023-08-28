using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour {

	public float Speed = 5;
	public Vector3 Trajectory;

	void Start () {
	
	}

	void Update () {
		transform.Translate (Trajectory * Speed);
	}

	void OnCollisionEnter (Collision collision) {
		Destroy (gameObject);
	}
}

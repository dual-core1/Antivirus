using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float SpawnInterval = 2f;
	public GameObject prefab;
	public float spawnX;
	public float spawnMinY;
	public float spawnMaxY;

	float timer;

	void Start () {
		timer = 0f;
	}
	

	void Update () {
		timer += Time.deltaTime;

		if (timer >= SpawnInterval) {
			SpawnFile ();
			timer = 0f;
		}
	}

	void SpawnFile () {
		float randY = Random.Range (spawnMinY, spawnMaxY);
		GameObject file = (GameObject)Instantiate (prefab, new Vector3 (spawnX, randY, 0), Quaternion.identity);
	}
}

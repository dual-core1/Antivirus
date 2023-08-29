using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsPanel : MonoBehaviour {

	public Text healthText;
	public Text scoreText;
	public Text stageText;
	public PlayerController player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = "Health: " + player.Health.ToString ();
		scoreText.text = "Score: " + player.score.ToString ();
		stageText.text = "Stage: " + player.stage.ToString ();
	}
}

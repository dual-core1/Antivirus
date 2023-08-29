using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public PlayerController player;
	public Image StageComplete;
	public Image GameOver;
	public AudioSource StageCompleteTheme;
	public AudioSource GameOverTheme;
	public AudioSource GameTheme;
	bool DidGameOver;
	bool DidStageAdvance;
	float StageAdvanceTimer;

	int[] score_thresh = {
		0, 300, 700, 1200, 1800, 2500, 3300, 4200, 5200, 7200, 10200
	};

	void Start () {
		DidGameOver = false;
		DidStageAdvance = false;
		StageAdvanceTimer = 0f;
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		StageComplete.enabled = false;
		GameOver.enabled = false;
	}
	

	void Update () {
		// game over conditions

		// negative score
		if (player.score < 0 && !DidGameOver) {
			DidGameOver = true;
			DoGameOver ();
		}

		// player dead
		if (player.Health < 1 && !DidGameOver) {
			DidGameOver = true;
			DoGameOver ();
		}

		// stage advance condition
		if (player.score >= score_thresh[player.stage] && !DidStageAdvance) {
			DidStageAdvance = true;
			DoStageAdvance ();
		}
	}

	void DoGameOver () {
		player.MoveSpeed = 0f;
		GameOver.enabled = true;
		GameOverTheme.Play ();
	}

	void DoStageAdvance () {

	}
}

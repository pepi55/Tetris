using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	void OnGUI () {
		GUI.Label(new Rect (600, 100, 100, 100), "SCORE: " + GlobalVariables.score);

		if (Block.gameOver) {
			GUI.Label(new Rect(100, 100, 100, 100), "GAME OVER");
		}
	}
}
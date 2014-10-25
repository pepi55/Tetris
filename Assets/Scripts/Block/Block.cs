using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public delegate void DeleteParent();
	public static event DeleteParent deleteParent;

	private float tickDown = 0;
	public static bool gameOver = false;

	// Use this for initialization
	void Start () {
		if (!validPosition()) {
			gameOver = true;
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.position += new Vector3(-1.0f, 0.0f, 0.0f);

			if (validPosition()) {
				updateGrid();
			} else {
				transform.position += new Vector3(1.0f, 0.0f, 0.0f);
			}
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.position += new Vector3(1.0f, 0.0f, 0.0f);

			if (validPosition()) {
				updateGrid();
			} else {
				transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			transform.Rotate(0.0f, 0.0f, 90.0f);

			if (validPosition()) {
				updateGrid();
			} else {
				transform.Rotate(0.0f, 0.0f, -90.0f);
			}
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - tickDown >= 1) {
			transform.position += new Vector3(0.0f, -1.0f, 0.0f);

			if (validPosition()) {
				updateGrid();
			} else {
				transform.position += new Vector3(0.0f, 1.0f, 0.0f);

				Grid.deleteFullRows();

				if (deleteParent != null) {
					deleteParent();
				}

				FindObjectOfType<Spawn>().SpawnBlock();
				Destroy(gameObject);
			}

			tickDown = Time.time;
		}
	}

	private bool validPosition () {
		foreach (Transform child in transform) {
			Vector3 vec = Grid.roundVector3(child.position);

			if (!Grid.insideBorder(vec)) {
				return false;
			}

			if (Grid.grid[(int)vec.x, (int)vec.y] != null) {
				if (Grid.grid[(int)vec.x, (int)vec.y].parent != transform) {
					return false;
				}
			}
		}

		return true;
	}

	private void updateGrid () {
		for (int y = 0; y < Grid.h; ++y) {
			for (int x = 0; x < Grid.w; ++x) {
				if (Grid.grid[x, y] != null) {
					if (Grid.grid[x, y].parent == transform) {
						Grid.grid[x, y] = null;
					}
				}
			}
		}

		foreach (Transform child in transform) {
			Vector3 vec = Grid.roundVector3(child.position);

			Grid.grid[(int)vec.x, (int)vec.y] = child;
		}
	}
}
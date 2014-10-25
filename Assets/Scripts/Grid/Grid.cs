using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	public static int w = GlobalVariables.gridWidth;
	public static int h = GlobalVariables.gridHeight;
	public static Transform[,] grid = new Transform[w, h];

	public static Vector3 roundVector3 (Vector3 vec) {
		return new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
	}

	public static bool insideBorder (Vector3 pos) {
		return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
	}

	public static void deleteRow (int y) {
		for (int x = 0; x < w; ++x) {
			Destroy(grid[x, y].gameObject);

			grid[x, y] = null;
		}
	}

	private static void decreaseRow (int y) {
		for (int x = 0; x < w; ++x) {
			if (grid[x, y] != null) {
				grid[x, y - 1] = grid[x, y];
				grid[x, y] = null;

				grid[x, y - 1].position += new Vector3(0.0f, -1.0f, 0.0f);
			}
		}
	}

	public static void decreaseLevelRows (int y) {
		for (int i = y; i < h; ++i) {
			decreaseRow(i);
		}
	}

	public static bool fullRow (int y) {
		for (int x = 0; x < w; ++x) {
			if (grid[x, y] == null) {
				return false;
			}
		}

		return true;
	}

	public static void deleteFullRows () {
		for (int y = 0; y < h; ++y) {
			if (fullRow(y)) {
				deleteRow(y);
				decreaseLevelRows(y + 1);

				--y;
			}
		}
	}
}
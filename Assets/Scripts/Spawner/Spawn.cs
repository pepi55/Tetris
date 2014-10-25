using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public GameObject[] blocks;
	private Vector3 spawnLocation = new Vector3(6, 15, 0);

	void Start () {
		SpawnBlock();
	}

	public void SpawnBlock () {
		int i = Random.Range(0, blocks.Length);
		GameObject block = (GameObject)Instantiate(blocks[i], spawnLocation, Quaternion.identity);

		block.tag = GlobalVariables.blockTag;
	}
}
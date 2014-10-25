using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	void OnEnable () {
		Block.deleteParent += removeParent;
	}

	void OnDisable () {
		Block.deleteParent -= removeParent;
	}

	public void removeParent () {
		this.transform.parent = null;
	}
}

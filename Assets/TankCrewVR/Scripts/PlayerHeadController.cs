using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadController : MonoBehaviour {
	private GameObject playerHeadCam;

	//TODO Move this crap
	private GameObject spawn;
	private GameObject playerRig;

	// Use this for initialization
	void Start () {
		playerHeadCam = GameObject.Find("Camera (eye)");
		playerRig = GameObject.Find("PlayerRig");

		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
		spawn = spawns[0];
		spawn.transform.tag = "Untagged";

		playerRig.transform.SetParent(spawn.transform);
		playerRig.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = playerHeadCam.transform.position;
	}
}
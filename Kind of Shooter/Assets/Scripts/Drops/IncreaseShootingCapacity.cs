using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseShootingCapacity : MonoBehaviour {

	public int increaseAmount = 1;

	GameObject player;
	PlayerShooting playerShooting;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindWithTag ("Player");
		playerShooting = player.GetComponentInChildren <PlayerShooting> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if (other.gameObject == player) {
			IncreaseCapacityDisplayManager.DisplayIncreaseAmount (increaseAmount);
			playerShooting.magazineCapacity += increaseAmount;
			playerShooting.UpdateCapacity (increaseAmount);
			Destroy (gameObject);
		}
	}
}

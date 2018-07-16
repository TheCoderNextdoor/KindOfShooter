using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeRestore : MonoBehaviour {


	public int restoreAmount = 5;

	GameObject player;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}


	void OnTriggerEnter (Collider other)
	{
		//regenerate health, display the animation and destroy the object
		if(other.gameObject == player)
		{
			if (!playerHealth.HealthCapacityCheck ()) {
				HealthRestoreDispayManager.DisplayLifeRestoreAmount (restoreAmount);
				playerHealth.currentHealth += restoreAmount;
				Destroy (gameObject);
			}
		}
	}
}

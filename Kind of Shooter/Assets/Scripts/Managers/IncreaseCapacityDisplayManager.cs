using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseCapacityDisplayManager : MonoBehaviour {

	public static int increaseAmount;

	static Animator anim;
	static Text text;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		text = GetComponent <Text> ();
		increaseAmount = 0;
	}

	// Update is called once per frame
	public static void DisplayIncreaseAmount (int amount) {
		increaseAmount = amount;
		text.text = "+" + increaseAmount;
		anim.SetTrigger ("IncreaseCapacity");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRestoreDispayManager : MonoBehaviour {

	public static int restoreAmount;

	static Animator anim;
	static Text text;
	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator> ();
		text = GetComponent <Text> ();
		restoreAmount = 0;
	}
	
	// Update is called once per frame
	public static void DisplayLifeRestoreAmount (int amount) {
		restoreAmount = amount;
		text.text = "+" + restoreAmount;
		anim.SetTrigger ("LifeRestore");
	}
}

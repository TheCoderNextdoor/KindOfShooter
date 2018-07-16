using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour {

	public GameObject[] itemsList;
	public float dropAtHeight = 0.60f;
	int randomDropVariable = 0;


	public void DropSomething(Vector3 position, Quaternion rotation)
	{
		//set the height above ground to drop
		position.y = dropAtHeight;
		Debug.Log (position);
		//get a random number between 0 and 2
		randomDropVariable = (int)Random.Range(0, itemsList.Length);
		//spawn something
		if(itemsList[randomDropVariable] != null)
			Instantiate (itemsList[randomDropVariable], position, transform.rotation);
	}
}

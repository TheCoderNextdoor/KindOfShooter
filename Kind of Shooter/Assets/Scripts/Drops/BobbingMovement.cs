using UnityEngine;

public class BobbingMovement : MonoBehaviour {

	//speed of movemnet
	public float moveSpeed = 0.6f;
	//maximum movement
	public float moveAmplitude = 0.007f;

	//phase of the movement(randomly generated)
	float movePhase;

	Vector3 upVector;

	// Use this for initialization
	void Start () {
		//set the vector to (0, 1, 0)
		upVector = Vector3.up;
		//generate a random phase
		movePhase = Random.Range (-Mathf.PI, Mathf.PI);
	}

	// Update is called once per frame
	void FixedUpdate () {
		//change y of upVector between -moveAmplitude to moveAmplitude 
		upVector.y = Mathf.Sin (2 * Mathf.PI * Time.fixedTime * moveSpeed + movePhase)*moveAmplitude;
		transform.SetPositionAndRotation(transform.position + upVector, transform.rotation);
	}
}

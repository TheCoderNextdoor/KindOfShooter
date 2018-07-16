using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 7f;

	Vector3 movement;
	Animator anim;
	Rigidbody rigidBody;
	int floorMask;
	float camRayLength = 100f;

	//caled regardless of the script enabled
	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){

		//get values of either -1 0 or 1
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move(float h, float v){

		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		rigidBody.MovePosition (transform.position + movement);
	}

	void Turning(){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			rigidBody.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v){
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}

}

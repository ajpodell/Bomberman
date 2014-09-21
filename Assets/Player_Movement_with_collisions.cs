using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {
	public GameObject	bombPrefab;
	//public GameObject	bomb; 
	public Vector3 		velocity;
	public float 		speed = 10;
	public bool 		locked = false; 
	public bool 		goingRight = false; 
	public bool 		goingLeft = false; 
	public bool 		goingUp = false; 
	public bool 		goingDown = false; 


	public float 		timeSinceBombDrop	= 0.5f;
	//bool bombOnMap = false;
	
	// Use this for initialization
	void Start () {
		//bomb = null;
	}
	
	// Update is called once per frame
	void Update () {
		//velocity = Vector3.zero;
		Vector3 pos = transform.position;
		if (locked) {
			return; 
		}
		velocity.x = Input.GetAxis ("Horizontal");
		velocity.y = Input.GetAxis ("Vertical");
		pos += velocity * Time.deltaTime * speed;
		transform.position = pos; 
		timeSinceBombDrop += Time.deltaTime;
		
		if (Input.GetButton("Jump") && 
		    		(Game_State.bombsOnMap < Game_State.MAXBOMBS) &&
		    		(timeSinceBombDrop >= 0.5f)) {

			GameObject bomb = Instantiate(bombPrefab) as GameObject;
			bomb.transform.position = transform.position;
			Game_State.bombsOnMap++;
			timeSinceBombDrop = 0.0f;
		}
	}

	void OnTriggerEnter(Collider col) {
		locked = true; 
		if (velocity.x > 0) goingRight = true;
		else if (velocity.x < 0) goingLeft = true;
		else if (velocity.y > 0) goingUp = true;
		else if (velocity.y < 0) goingDown = true;
	}


	void OnTriggerStay(Collider col) {
		Vector3 pos = transform.position;
		velocity.y = Input.GetAxis("Vertical"); 
		velocity.x = Input.GetAxis ("Horizontal"); 
		if (velocity.x < 0 && goingLeft) {
			velocity.x = 0;
		}
		else if (velocity.x > 0 && goingRight) { 
			velocity.x = 0;
		}
		if (velocity.y < 0 && goingDown) {
			velocity.y = 0;
		}
		if (velocity.y > 0 && goingUp) {
			velocity.y = 0;
		}
		pos += velocity * Time.deltaTime * speed; 
		transform.position = pos; 
	}
	 
	void OnTriggerExit(Collider col) {
		locked = false; 
		goingRight = false; 
		goingLeft = false; 
		goingUp = false; 
		goingDown = false; 
	}
	
}





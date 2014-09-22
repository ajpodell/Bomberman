using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {
	public GameObject	bombPrefab;
	//public GameObject	bomb; 
	public Vector3 		velocity;
	public float 		speed = 10;
	public bool 		locked = false;  
	public bool 		goinginX = false; 
	public bool 		goinginY = false;
	public bool 		cantGoRight = false; 
	public bool 		cantGoLeft = false;
	public bool 		cantGoDown = false; 
	public bool			cantGoUp = false;
	public int 			numCollwith = 0; 

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
		Vector3 wallPos = col.gameObject.transform.position; 
		Vector3 manPos = transform.position; 
		locked = true; 
		col.gameObject.renderer.material.color = Color.red;
		numCollwith++; 
		if (velocity.x != 0) goinginX = true;
		if (velocity.y != 0) goinginY = true;
		if (goinginX) {
			// first stop motion in direction of wall 
			if (wallPos.x > manPos.x) { // wall on left 
				cantGoRight = true; 
			}
			if (wallPos.x < manPos.x) { // wall on right 
				cantGoLeft = true; 
			}
			// check if passed y direction boundaries
			if (manPos.y > Game_State.MAP_HEIGHT - 2) {
				cantGoUp = true; 
			}
			if (manPos.y < 2) {
				cantGoDown = true; 
			}
		}
		if (goinginY) {
			// stop motion in direction of wall 
			if (wallPos.y > manPos.y) { // wall above
				cantGoUp = true; 
			}
			if (wallPos.y < manPos.y) { // wall below 
				cantGoDown = true;
			}
			// check if passed x direction boundaries
			if (manPos.x > Game_State.MAP_WIDTH + 2) {
				cantGoRight = true;
			}
			if (manPos.x < 2) {
				cantGoLeft = true;
			}
		}
	}


	void OnTriggerStay(Collider col) {
		Vector3 pos = transform.position;
		velocity.y = Input.GetAxis("Vertical"); 
		velocity.x = Input.GetAxis ("Horizontal"); 
		if (velocity.x < 0 && cantGoLeft) {
			velocity.x = 0;
		}
		if (velocity.x > 0 && cantGoRight) { 
			velocity.x = 0;
		}
		if (velocity.y < 0 && cantGoDown) {
			velocity.y = 0;
		}
		if (velocity.y > 0 && cantGoUp) {
			velocity.y = 0;
		}
		pos += velocity * Time.deltaTime * speed; 
		transform.position = pos; 
	}
	 
	void OnTriggerExit(Collider col) {
		numCollwith--;
		if (numCollwith == 0) {
			locked = false;
			goinginX = false; 
			goinginY = false; 
			cantGoUp = false; 
			cantGoDown = false;
			cantGoLeft = false; 
			cantGoRight = false; 
		}
		col.gameObject.renderer.material.color = Color.grey; 
	}
	
}





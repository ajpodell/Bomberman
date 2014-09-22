using UnityEngine;
using System.Collections;

public class Player_Movement_with_collisions : MonoBehaviour {
	public GameObject	bombPrefab;
	public GameObject 	wallPrefab; 
	public GameObject 	collidingObject; 
	public Vector3 		velocity;
	public float 		speed = 10;
	public bool 		goinginX = false; 
	public bool 		goinginY = false;
	public bool 		cantGoRight = false; 
	public bool 		cantGoLeft = false;
	public bool 		cantGoDown = false; 
	public bool			cantGoUp = false;
	public int 			numCollwith = 0; 
	public float 		bounceBackdist = 0.02f; 
	public float 		timeSinceBombDrop = 0.5f;
	public float 		bottomofMan = 0.0f;
	public float 		topofMan = 0.0f; 
	public float 		leftofMan = 0.0f; 
	public float 		rightofMan = 0.0f;
	public float 		bottomofWall = 0.0f; 
	public float 		topofWall = 0.0f; 
	public float 		leftofWall = 0.0f; 
	public float 		rightofWall = 0.0f; 
	//bool bombOnMap = false;
	
	// Use this for initialization
	void Start () {
		//bomb = null;
	}
	
	// Update is called once per frame
	void Update () {
		//velocity = Vector3.zero;
		Vector3 pos = transform.position;
//		if (locked) {
//			return; 
//		}
		velocity.x = Input.GetAxis ("Horizontal");
		velocity.y = Input.GetAxis ("Vertical");
		if (velocity.x > 0) cantGoLeft = false;
		if (velocity.x < 0) cantGoRight = false;
		if (velocity.y > 0) cantGoDown = false;
		if (velocity.y < 0) cantGoUp = false;

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
		timeSinceBombDrop += Time.deltaTime;

		if (collidingObject != null) {
			checkIfUnlockedNeeded (collidingObject); 
		}
		
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
		if (col.gameObject.tag == "bomb_child")
						return;

		collidingObject = col.gameObject;
		Vector3 wallPos = col.gameObject.transform.position; 
		Vector3 manPos = transform.position; 
//		col.gameObject.renderer.material.color = Color.red;
		if (velocity.x != 0) goinginX = true;
		if (velocity.y != 0) goinginY = true;
		if (goinginX) {
			// stop motion in direction of wall 
			if (wallPos.x > manPos.x) { // wall on right 
				cantGoRight = true; 
				float newxpos = wallPos.x - (col.gameObject.transform.lossyScale.x / 2) - 
								(transform.lossyScale.x / 2) - bounceBackdist; 
				manPos.x = newxpos;
				transform.position = manPos; 
			}
			if (wallPos.x < manPos.x) { // wall on left 
				cantGoLeft = true; 
				float newxpos = wallPos.x + (col.gameObject.transform.lossyScale.x / 2) + 
					(transform.lossyScale.x / 2) + bounceBackdist; 
				manPos.x = newxpos;
				transform.position = manPos; 
			}
		}
		if (goinginY) {
			// stop motion in direction of wall 
			if (wallPos.y > manPos.y) { // wall above
				cantGoUp = true; 
				float newypos = wallPos.y - (col.gameObject.transform.lossyScale.y / 2) - 
					(transform.lossyScale.y / 2) - bounceBackdist; 
				manPos.y = newypos;
				transform.position = manPos; 
			}
			if (wallPos.y < manPos.y) { // wall below 
				cantGoDown = true;
				float newypos = wallPos.y + (col.gameObject.transform.lossyScale.y / 2) + 
					(transform.lossyScale.y / 2) + bounceBackdist; 
				manPos.y = newypos;
				transform.position = manPos;
			}
		}
	}

	
	void OnTriggerExit(Collider col) {

		if (col.gameObject.tag == "bomb_child") {
			col.gameObject.transform.parent.gameObject.layer = 0; 
			//Debug.Log(col.gameObject.layer.ToString()); 
			return;
		}
//		if (numCollwith == 0) {
//			locked = false;
//			goinginX = false; 
//			goinginY = false; 
//			cantGoUp = false; 
//			cantGoDown = false;
//			cantGoLeft = false; 
//			cantGoRight = false; 
//		}
		goinginX = false; 
		goinginY = false; 
		col.gameObject.renderer.material = wallPrefab.gameObject.renderer.material; 

	}

	void checkIfUnlockedNeeded(GameObject col) {
		Vector3 wallPos = col.gameObject.transform.position; 
		Vector3 manPos = transform.position;
		bottomofMan = manPos.y - (transform.lossyScale.y / 2);
		topofMan = manPos.y + (transform.lossyScale.y / 2);
		leftofMan = manPos.x - (transform.lossyScale.x / 2); 
		rightofMan = manPos.x + (transform.lossyScale.x / 2); 
		bottomofWall = wallPos.y - (col.transform.lossyScale.y / 2); 
		topofWall = wallPos.y + (col.transform.lossyScale.y / 2); 
		leftofWall = wallPos.x - (col.transform.lossyScale.x / 2); 
		rightofWall = wallPos.x + (col.transform.lossyScale.x / 2); 
		//if (velocity.x != 0) {
			if ((topofMan < bottomofWall) || (bottomofMan > topofWall)) {
				cantGoLeft = false; 
				cantGoRight = false; 
			}
		//}
		//if (goinginY) {
			if ((rightofMan < leftofWall) || (leftofMan > rightofWall)) {
				cantGoUp = false; 
				cantGoDown = false; 
			}
//		}
	}

	void OnTriggerStay(Collider col) {
			OnTriggerEnter (col);
	}
}





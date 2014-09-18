using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {
	public GameObject	bombPrefab;
	//public GameObject	bomb; 
	public Vector3 		velocity;
	public float 		speed = 10;


	public float 		timeSinceBombDrop	= 0.5f;
	//bool bombOnMap = false;
	
	// Use this for initialization
	void Start () {
		//bomb = null;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		velocity.x = Input.GetAxis ("Horizontal") * speed;
		velocity.y = Input.GetAxis ("Vertical") * speed;
		pos += velocity * Time.deltaTime;
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
		
		/*
		if ( Input.GetKey("space") ) {
			GameObject bomb = 
		*/
	}
}
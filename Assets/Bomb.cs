using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	private float TimeToExplosion 	= 2.0f;
	private float timeAlive 		= 0.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive += Time.deltaTime;
		if( timeAlive >= TimeToExplosion ) {
			Destroy(this.gameObject);
			Game_State.bombsOnMap--; 
		}
	}
	
}
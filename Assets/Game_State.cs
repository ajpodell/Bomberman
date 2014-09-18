using UnityEngine;
using System.Collections;

public class Game_State : MonoBehaviour {
	public GameObject	permWallPrefab;


	public static int MAXBOMBS 		= 3;
	public static int bombsOnMap 	= 0;

	// Use this for initialization
	void Start () {
		// use 4 loops to instantiate a perimeter of walls
		for( int x = 0; x < 19; ++x ) {
			for( int y = 0; y < 11; ++y) {
				if( x == 0 || y == 0 || x == 18 || y == 10 || (x % 2 == 0 && y % 2 == 0) ) {
					GameObject wall = Instantiate(permWallPrefab) as GameObject;
					Vector3 pos = wall.transform.position;
					pos.x = x; pos.y = y; pos.z = 0;
					wall.transform.position = pos;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}

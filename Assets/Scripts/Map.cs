using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	GameObject tile;
	void GenerateTerrain () {
		GameObject tileClone = (GameObject)Instantiate (tile);
		int ringScope = 2;
		int ringCurrent = 1;
		float apothem;
		int apothemMultiplyer = 2;
		float tileAngle = 30;
		float tileAngleIncrement = 60;
		// rings
		for (int i = 0; i < ringScope; i++) {
			// tiles for each ring
			for (int j = 0; j < 6 * ringCurrent; j++) {
				apothem = Mathf.Cos(30 * Mathf.PI / 180) * apothemMultiplyer;
				tileClone = (GameObject)Instantiate (
					tile, 
					new Vector3 (apothem * Mathf.Cos(tileAngle * Mathf.PI / 180), 0, apothem * Mathf.Sin(tileAngle * Mathf.PI / 180)),
					Quaternion.identity);
				tileAngle += tileAngleIncrement;
			}
			ringCurrent++;
			tileAngleIncrement /= 2;
			apothemMultiplyer += 2;
		}

	}
	void Start () {
		tile = (GameObject)Resources.Load ("Prefabs/Tile");
		GenerateTerrain ();
	}
	void Update () {
	
	}
}
